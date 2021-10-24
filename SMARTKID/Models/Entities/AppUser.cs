using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            this.Kids = new List<Kid>();
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gendre { get; set; }

        public string PhotoPath { get; set; }

        public string CIN { get; set; }

        public string CinCopyPath { get; set; }

        public string AddressLine_1 { get; set; }

        public string AddressLine_2 { get; set; }

        public string PhoneNumber_2 { get; set; }

        public string PostalCode { get; set; }

        public ICollection<Kid> Kids { get; set; }
    }
}
