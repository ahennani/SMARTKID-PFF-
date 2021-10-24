using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.ViewModels
{
    public class AccountSignInViewModel
    {
        public AccountSignInViewModel()
        {
            this.ExternalLogins = new ExternalLoginsViewModel();
        }

        [Required(ErrorMessage = "Email Is Required")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remembre me")]
        public bool Remembre { get; set; }

        public ExternalLoginsViewModel ExternalLogins { get; set; }
    }
}
