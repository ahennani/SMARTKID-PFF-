using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.Models.Entities
{
    public class TeacherKid
    {
        public int KidID { get; set; }
        public Kid Kid { get; set; }

        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }
    }
}
