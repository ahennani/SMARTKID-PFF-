using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.ViewModels
{
    public class AccountSignUpViewModel
    {
        public AccountSignUpViewModel()
        {
            this.ExternalLogins = new ExternalLoginsViewModel();
        }


        [Required(ErrorMessage = "First Name Is Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date Of Birth Is Required")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gendre Is Required")]
        [Display(Name = "Gendre")]
        public string Gendre { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [Display(Name = "Email")]
        [Remote(action: "CheckEmailExistance", controller: "Account")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Password Confirmation")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Please Entre a Match Password!!")]
        public string ConfirmPassword { get; set; }

        public ExternalLoginsViewModel ExternalLogins { get; set; }
    }
}
