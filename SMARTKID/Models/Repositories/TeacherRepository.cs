using Microsoft.EntityFrameworkCore;
using SMARTKID.App_Data;
using SMARTKID.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.Models.Repositories
{
    public class TeacherRepository : ISchoolRepository<Teacher>
    {
        private readonly AppDbContext _appDbContext;

        public TeacherRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public Teacher Add(Teacher teacher)
        {
            //teacher.PhotoPath = "/Images/emp.jpg";
            this._appDbContext.Teachers.Add(teacher);
            this._appDbContext.SaveChanges();
            return teacher;
        }

        public Teacher Delete(int id)
        {
            var teacher = this.Get(id);
            if (!(teacher == null))
            {
                this._appDbContext.Teachers.Remove(teacher);
                this._appDbContext.SaveChanges();
            }
            return teacher;
        }

        public Teacher Get(int id)
        {
            var teacher = this._appDbContext.Teachers.SingleOrDefault(tch => tch.TeacherID == id);
            return teacher;
        }

        public IEnumerable<Teacher> List()
        {
            return this._appDbContext.Teachers;
        }

        public Teacher Update(int id, Teacher oldTeacher)
        {
            var teacher = this._appDbContext.Teachers.Attach(oldTeacher);
            teacher.State = EntityState.Modified;
            this._appDbContext.SaveChanges();
            return oldTeacher;
        }
    }
}
