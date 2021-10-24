using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using SMARTKID.Models;
using SMARTKID.ViewModels;
using Microsoft.AspNetCore.Http;
using SMARTKID.Models.Repositories;
using SMARTKID.App_Data;
using SMARTKID.Models.Entities;

namespace SMARTKID.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ISchoolRepository<Kid> _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController
                    (
                        AppDbContext db,
                        UserManager<AppUser> userManager,
                        SignInManager<AppUser> signInManager,
                        ISchoolRepository<Kid> context,
                        ILogger<AccountController> logger,
                        IWebHostEnvironment webHostEnvironment
                    )
        {
            this._db = db;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._context = context;
            this._logger = logger;
            this._webHostEnvironment = webHostEnvironment;
        }

        //////////////////////////////// <SignUp> ///////////////////////////////////
        [HttpGet]
        [AllowAnonymous]
        [Route("~/[action]")]
        //[Route("~/[controller]/[action]")]
        public async Task<IActionResult> SignUp(string returnUrl)
        {
            if (this._signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new AccountSignUpViewModel()
            {
                DateOfBirth = DateTime.Now,
                ExternalLogins = new ExternalLoginsViewModel()
                {
                    ReturnUrl = returnUrl,
                    ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
                }
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("~/[action]")]
        public async Task<IActionResult> SignUp(AccountSignUpViewModel model)
        {
            model.DateOfBirth = (model.DateOfBirth.Year == 1 ? DateTime.Now : model.DateOfBirth);

            model.ExternalLogins
                 .ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gendre = model.Gendre,
                    DateOfBirth = model.DateOfBirth,
                    Email = model.Email
                };

                var result = await this._userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // isPersistent if true keeps us signed in even when close the browser
                    await this._signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        //////////////////////////////// <SignIn> ///////////////////////////////////
        [HttpGet]
        [AllowAnonymous]
        [Route("~/[action]")]
        //[Route("~/[controller]/[action]")]
        public async Task<IActionResult> SignIn(string returnUrl)
        {
            var model = new AccountSignInViewModel();
            model.ExternalLogins.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            model.ExternalLogins.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("~/[action]")]
        //[Route("~/[controller]/[action]")]
        public async Task<IActionResult> SignIn(AccountSignInViewModel model, string ReturnUrl)
        {
            if (this._signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }

            model.ExternalLogins = new ExternalLoginsViewModel()
            {
                ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (ModelState.IsValid)
            {
                // Change The Last Parametr To Activate The AccountLock After A specific Tries.
                var result = await this._signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remembre, false);
                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        // Will return the user to any URL.
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Email or Password Incorrect!!");
            }
            return View(model);
        }

        //////////////////////////////// <ExternalLogin> ///////////////////////////////////
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl) // provider: name of button
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });

            var properties = this._signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            var login = new AccountSignUpViewModel()
            {
                ExternalLogins = new ExternalLoginsViewModel()
                {
                    ReturnUrl = returnUrl,
                    ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
                }
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from External Provider: {remoteError}");
                return View(nameof(SignIn), login);
            }

            var info = await this._signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error Loading External Login Information.");
                return View(nameof(SignIn), login);
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            AppUser user = null;
            if (email != null)
            {
                user = await this._userManager.FindByEmailAsync(email);
            }

            var signInResult = await this._signInManager.ExternalLoginSignInAsync
                                        (
                                            info.LoginProvider,
                                            info.ProviderKey,
                                            isPersistent: false,
                                            bypassTwoFactor: true
                                        );

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                if (email != null)
                {
                    if (user == null)
                    {
                        var firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
                        var lastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
                        var name = info.Principal.FindFirstValue(ClaimTypes.Name);
                        var gendre = info.Principal.FindFirstValue(ClaimTypes.Gender);
                        user = new AppUser
                        {
                            UserName = email,
                            Email = email,
                            FirstName = firstName == null ? "User" : firstName,
                            LastName = lastName == null ? "User" : lastName,
                            Gendre = gendre == null ? "unset" : gendre
                        };

                        var generatedUserPassword = GenerateRandomPassword();

                        var result = await this._userManager.CreateAsync(user, generatedUserPassword);
                        if (!result.Succeeded)
                        {
                            return RedirectToAction(nameof(SignUp), login);
                        }

                        var token = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action(
                                                            "ConfirmEmail",
                                                            "Account",
                                                            new { userId = user.Id, token = token },
                                                            Request.Scheme
                                                         );
                        this._logger.LogWarning
                            (
                                $"\n\n\n" +
                                $"\nUser: {email}" +
                                $"\nGenerated Password: \"{generatedUserPassword}\"" +
                                $"\nGenerated Email Confirmation: \"{confirmationLink}\"" +
                                $"\n\n\n"
                            );


                    }

                    await this._userManager.AddLoginAsync(user, info);
                    await this._signInManager.SignInAsync(user, isPersistent: true);

                    return LocalRedirect(returnUrl);
                }

                var modl = new StatusResultViewModel();
                modl.Title = $"Email claim not received from: {info.LoginProvider}";
                modl.Message = "Please contact support on test@test.com";

                return View("../Error/NotFound", modl);
            }
        }


        //////////////////////////////// <Edit Account> ///////////////////////////////////
        [HttpGet]
        [Route("~/[action]")]
        public async Task<IActionResult> EditAccount(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var user = await this._userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var model = new AccountEditAccountViewModel()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        DateOfBirth = user.DateOfBirth,
                        Email = user.Email,
                        Gendre = user.Gendre
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("~/[action]")]
        public async Task<IActionResult> EditAccount(AccountEditAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Gendre = model.Gendre;
                    user.DateOfBirth = model.DateOfBirth;
                    if (model.Password != null)
                    {
                        // This Method Hash password before assigning it to a user..
                        var password = this._userManager.PasswordHasher.HashPassword(user, model.Password);
                        user.PasswordHash = password;
                    }

                    var result = await this._userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }


        //////////////////////////////// <Logout> ///////////////////////////////////
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        //////////////////////////////// <Inscription> ///////////////////////////////////
        [HttpGet]
        [AllowAnonymous]
        [Route("~/[action]")]
        //[Route("~/[controller]/[action]")]
        public IActionResult Inscription()
        {
            var model = new AccountInscriptionViewModel();
            model.GuardianDateOfBirth = DateTime.Now;
            model.KidDateOfBirth = DateTime.Now;
            if (this._signInManager.IsSignedIn(User))
            {
                var user = this._userManager.FindByNameAsync(User.Identity.Name).Result;
                model.GuardianFirstName = user.FirstName;
                model.GuardianLastName = user.LastName;
                model.GuardianDateOfBirth = user.DateOfBirth;
                model.GuardianGendre = user.Gendre;
                model.GuardianDateOfBirth = user.DateOfBirth;
                model.Email = user.Email;
                model.PhoneNumber_1 = user.PhoneNumber;
                model.PhoneNumber_2 = user.PhoneNumber_2;
                model.AddressLine_1 = user.AddressLine_1;
                model.AddressLine_2 = user.AddressLine_2;
                model.PostalCode = user.PostalCode;
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("~/[action]")]
        //[Route("~/[controller]/[action]")]
        public async Task<IActionResult> Inscription(AccountInscriptionViewModel model)
        {
            if (this._signInManager.IsSignedIn(User))
            {
                var user = await this._userManager.FindByNameAsync(User.Identity.Name);
                if (user.Email != model.Email.Trim())
                {
                    ModelState.AddModelError(string.Empty, "Email Should Mathc Signed User!!");
                    return View(model);
                }
            }

            if (ModelState.IsValid)
            {
                string fullName = null;
                string filePath = null;
                var folders = new string[] { "Guardians", "GuardianCINs", "Kids" };
                var exts = new String[] { ".jpg", ".png", ".jpeg" };

                // Fill Guardian Info
                var extFilePhoto = Path.GetExtension(model.GuardianPhoto.FileName).ToLower();
                if (!(exts.Contains(extFilePhoto)))
                {
                    ModelState.AddModelError(string.Empty, "Invalid Image Extention For (Guardian Photo)!!");
                    return View(model);
                }
                fullName = $"{model.KidFirstName}_{model.KidLastName}";
                filePath = SerializeFile(model.KidPhoto, folders[0], fullName);

                var extFileCinCopy = Path.GetExtension(model.CinCopy.FileName).ToLower();
                if (!(extFileCinCopy.Contains(".pdf")))
                {
                    ModelState.AddModelError(string.Empty, "Invalid File Extention For (CIN Copy)!!");
                    return View(model);
                }
                var cinCopyPath = SerializeFile(model.CinCopy, folders[1], fullName);

                AppUser user = null;

                if (this._signInManager.IsSignedIn(User))
                {
                    user = await this._userManager.FindByNameAsync(User.Identity.Name);
                    user.FirstName = model.GuardianFirstName;
                    user.LastName = model.GuardianLastName;
                    user.Gendre = model.GuardianGendre;
                    user.CIN = model.CIN;
                    user.CinCopyPath = cinCopyPath;
                    user.DateOfBirth = model.GuardianDateOfBirth;
                    user.PhotoPath = filePath;
                    user.PhoneNumber = model.PhoneNumber_1;
                    user.PhoneNumber_2 = model.PhoneNumber_2;
                    user.AddressLine_1 = model.AddressLine_1;
                    user.AddressLine_2 = model.AddressLine_2;
                    user.PostalCode = model.PostalCode;

                    var result = await this._userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }
                }
                else
                {
                    user = new AppUser()
                    {
                        Email = model.Email,
                        UserName = model.Email,
                        CIN = model.CIN,
                        CinCopyPath = cinCopyPath,
                        FirstName = model.GuardianFirstName,
                        LastName = model.GuardianLastName,
                        DateOfBirth = model.GuardianDateOfBirth,
                        Gendre = model.GuardianGendre,
                        PhotoPath = filePath,
                        PhoneNumber = model.PhoneNumber_1,
                        PhoneNumber_2 = model.PhoneNumber_2,
                        AddressLine_1 = model.AddressLine_1,
                        AddressLine_2 = model.AddressLine_2,
                        PostalCode = model.PostalCode
                    };
                    var result = await this._userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await this._signInManager.SignInAsync(user, isPersistent: false);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }

                filePath = null;
                fullName = null;

                // Fill Kid Info
                extFilePhoto = Path.GetExtension(model.KidPhoto.FileName).ToLower();
                if (!(exts.Contains(extFilePhoto)))
                {
                    ModelState.AddModelError(string.Empty, "Invalid Image Extention For (Kid Photo)!!");
                    return View(model);
                }
                fullName = $"{model.KidFirstName}_{model.KidLastName}";
                filePath = SerializeFile(model.KidPhoto, folders[2], fullName); // Kids

                var kid = new Kid()
                {
                    FirstName = model.KidFirstName,
                    LastName = model.KidLastName,
                    Gendre = model.KidGendre,
                    DateofBirth = model.KidDateOfBirth,
                    PhotoPath = filePath,
                    AppUser = user
                };

                var addedKid = this._context.Add(kid);
                if (addedKid == null)
                {
                    ModelState.AddModelError(string.Empty, "The Kid Informations Did Not Register!!");
                    return View(model);
                }

                this._db.Inscriptions.Add(new Inscription
                {
                    AppUser = user,
                    Kid = kid,
                    InscriptionDate = DateTime.Now
                });
                this._db.SaveChanges();

                if (ModelState.ErrorCount == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        public string SerializeFile(IFormFile file, string folderName, string fullName)
        {
            string filePath = null;

            if (file != null && folderName != null)
            {
                var extFile = Path.GetExtension(file.FileName).ToLower();

                fullName = fullName == null ? $"{file.FileName}" : $"{fullName}{extFile}";

                string uniqueFieName = $"{Guid.NewGuid()}_{fullName}";

                filePath = Path.Combine("Images", "Users", folderName, uniqueFieName);

                string path = Path.Combine(this._webHostEnvironment.WebRootPath, filePath);

                using (var fileLoaction = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileLoaction);
                }
            }

            return filePath;
        }

        //////////////////////////////// <Methods> ///////////////////////////////////

        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckEmailExistance(string email)
        {

            if (this._signInManager.IsSignedIn(User))
            {
                var userSigned = await this._userManager.FindByNameAsync(User.Identity.Name);
                if (userSigned.Email.Equals(email))
                {
                    return Json(true);
                }
            }

            return Json
                (
                    this._userManager.FindByEmailAsync(email).Result == null
                    ? "true"
                    : string.Format("The Email {0} is Already Exist!!..", email)
                );
        }

        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckCINExistance(string cin)
        {
            return Json
                (
                    this._userManager.Users.Where(u => u.CIN == cin).FirstOrDefault() == null
                    ? "true"
                    : string.Format("The Email {0} is Already Exist!!..", cin)
                );
        }

        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
