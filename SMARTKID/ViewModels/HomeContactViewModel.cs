using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.ViewModels
{
    public class HomeContactViewModel
    {
        [Required(ErrorMessage = "First Name Is Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name Is Required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Objective Is Required")]
        [Display(Name = "Objective")]
        public string Objective { get; set; }

        [Required(ErrorMessage = "Message Is Required")]
        [Display(Name = "Your Message Here")]
        public string Message { get; set; }
    }
}
