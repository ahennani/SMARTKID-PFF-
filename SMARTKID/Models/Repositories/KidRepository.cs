using Microsoft.EntityFrameworkCore;
using SMARTKID.App_Data;
using SMARTKID.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.Models.Repositories
{
    public class KidRepository : ISchoolRepository<Kid>
    {
        private readonly AppDbContext _appDbContext;

        public KidRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public Kid Add(Kid kid)
        {
            this._appDbContext.Kids.Add(kid);
            this._appDbContext.SaveChanges();
            return kid;
        }

        public Kid Delete(int id)
        {
            var kid = this.Get(id);
            if (!(kid == null))
            {
                this._appDbContext.Kids.Remove(kid);
                this._appDbContext.SaveChanges();
            }
            return kid;
        }

        public Kid Get(int id)
        {
            var kid = this._appDbContext.Kids.SingleOrDefault(kd => kd.KidID == id);
            return kid;
        }

        public IEnumerable<Kid> List()
        {
            return this._appDbContext.Kids;
        }

        public Kid Update(int id, Kid oldKid)
        {
            var kid = this._appDbContext.Kids.Attach(oldKid);
            kid.State = EntityState.Modified;
            this._appDbContext.SaveChanges();
            return oldKid;
        }
    }
}
