using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.Models.Entities
{
    public class Inscription
    {
        public int KidID { get; set; }
        public Kid Kid { get; set; }

        public string AppUserID { get; set; }
        public AppUser AppUser { get; set; }

        public DateTime InscriptionDate { get; set; }
    }
}
