using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.Models.Entities
{
    public class Teacher
    {
        public Teacher()
        {
            this.TeacherKids = new List<TeacherKid>();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherID { get; set; }

        [Required]
        public string Designation { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<TeacherKid> TeacherKids { get; set; }
    }
}
