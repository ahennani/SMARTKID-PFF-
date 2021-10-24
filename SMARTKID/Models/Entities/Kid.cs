using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.Models.Entities
{
    public class Kid
    {
        public Kid()
        {
            this.TeacherKids = new List<TeacherKid>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KidID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateofBirth { get; set; }

        [Required]
        public string Gendre { get; set; }

        [Required]
        public string PhotoPath { get; set; }


        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<TeacherKid> TeacherKids { get; set; }
    }
}
