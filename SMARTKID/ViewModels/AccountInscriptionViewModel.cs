using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.ViewModels
{
    public class AccountInscriptionViewModel
    {
        // Child Infos

        [Required(ErrorMessage = "First Name Is Required")]
        [Display(Name = "First Name")]
        public string KidFirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        [Display(Name = "Last Name")]
        public string KidLastName { get; set; }

        [Required(ErrorMessage = "Date Of Birth Is Required")]
        [Display(Name = "Date Of Birth")]
        public DateTime KidDateOfBirth { get; set; }

        [Required(ErrorMessage = "Gendre Is Required")]
        [Display(Name = "Gendre")]
        public string KidGendre { get; set; }

        [Required(ErrorMessage = "Photo Is Required")]
        [Display(Name = "Photo")]
        public IFormFile KidPhoto { get; set; }


        // Guardian Infos

        [Required(ErrorMessage = "CIN Is Required")]
        [Display(Name = "N° CIN")]
        [MinLength(5, ErrorMessage = "N°CIN Should Contain 5 Chars at least")]
        [MaxLength(8, ErrorMessage = "N°CIN Should Contain Less Than 5 Chars")]
        [Remote(action: "CheckCINExistance", controller: "Account")]
        public string CIN { get; set; }

        [Required(ErrorMessage = "CinCopy Is Required")]
        [Display(Name = "CIN Copy (PDF)")]
        public IFormFile CinCopy { get; set; }

        [Required(ErrorMessage = "First Name Is Required")]
        [Display(Name = "First Name")]
        public string GuardianFirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        [Display(Name = "Last Name")]
        public string GuardianLastName { get; set; }

        [Required(ErrorMessage = "Date Of Birth Is Required")]
        [Display(Name = "Date Of Birth")]
        public DateTime GuardianDateOfBirth { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [Display(Name = "Email Address")]
        [Remote(action: "CheckEmailExistance", controller: "Account")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gendre Is Required")]
        [Display(Name = "Gendre")]
        public string GuardianGendre { get; set; }

        [Required(ErrorMessage = "Photo Is Required")]
        [Display(Name = "Photo")]
        public IFormFile GuardianPhoto { get; set; }

        [Required(ErrorMessage = " Address Line N°1 Is Required")]
        [Display(Name = "Address Line N°1")]
        public string AddressLine_1 { get; set; }

        [Display(Name = "Address Line N°2")]
        public string AddressLine_2 { get; set; }

        [Required(ErrorMessage = "Phone Number N°1 Is Required")]
        [Display(Name = "Phone Number N°1")]
        [RegularExpression(pattern : @"^(\+212|0)([ \-_/]*)(\d[ \-_/]*){9}$", ErrorMessage = "Invalide Phone Number Format")]
        public string PhoneNumber_1 { get; set; }

        [Display(Name = "Phone Number N°2")]
        [RegularExpression(pattern : @"^(\+212|0)([ \-_/]*)(\d[ \-_/]*){9}$", ErrorMessage = "Invalide Phone Number Format")]
        public string PhoneNumber_2 { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }


        [Required(ErrorMessage = "Password Is Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Password Confirmation")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Please Entre a Match Password!!")]
        public string ConfirmPassword { get; set; }
    }
}
