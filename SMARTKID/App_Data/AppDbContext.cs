using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMARTKID.Models;
using SMARTKID.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMARTKID.App_Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        // Table Kid
        public DbSet<Kid> Kids { get; set; }

        // Table Teacher
        public DbSet<Teacher> Teachers { get; set; }

        // Table TeacherKid
        public DbSet<TeacherKid> TeacherKids { get; set; }

        // Table Inscription
        public DbSet<Inscription> Inscriptions { get; set; }

        // Table Contact
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Config AppUser Table
            builder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(t => t.CIN)
                      .IsUnique();

                entity.HasIndex(t => t.CIN)
                      .IsUnique();

                entity.HasCheckConstraint("CK_AppUser_Gendre", "[Gendre] IN ('Female', 'Male', 'unset')");

                entity.Property(p => p.Gendre)
                       .HasDefaultValue("unset")
                       .HasMaxLength(6);

                entity.Property(p => p.CIN)
                       .HasMaxLength(8);
            });

            // Config Kid Table
            builder.Entity<Kid>(entity =>
            {
                entity.ToTable("Kid");

                entity.HasKey(k => k.KidID)
                      .HasName("PK_Kid_KidID");

                entity.HasOne<AppUser>(k => k.AppUser)
                     .WithMany(ap => ap.Kids)
                     .HasForeignKey(k => k.AppUserId)
                     .IsRequired();
            });

            //Config Teacher Table
            builder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.HasKey(k => k.TeacherID)
                      .HasName("PK_Teacher_TeacherID");

                entity.HasOne(i => i.AppUser)
                      .WithMany()
                      .HasForeignKey(ap => ap.AppUserId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired();
            });

            // Config TeacherKid Table
            builder.Entity<TeacherKid>(entity =>
            {
                entity.ToTable("TeacherKid");

                entity.HasKey(tk => new { tk.KidID, tk.TeacherID })
                      .HasName("PK_TeacherKid");

                entity.HasOne<Kid>(tk => tk.Kid)
                      .WithMany(k => k.TeacherKids)
                      .HasForeignKey(tk => tk.KidID)
                      .OnDelete(DeleteBehavior.NoAction)
                      .IsRequired();

                entity.HasOne<Teacher>(tk => tk.Teacher)
                      .WithMany(k => k.TeacherKids)
                      .HasForeignKey(tk => tk.TeacherID)
                      .IsRequired();
            });

            // Config Inscription Table
            builder.Entity<Inscription>(entity =>
            {
                entity.ToTable("Inscription");

                entity.Property(b => b.InscriptionDate)
                       .HasDefaultValueSql("getdate()");

                entity.HasKey(i => new { i.KidID, i.AppUserID })
                      .HasName("PK_Inscription_AppUser_Kid");

                entity.HasOne(i => i.Kid)
                      .WithMany()
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(i => i.AppUser)
                      .WithMany()
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Config Contact Table
            builder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.HasKey(k => k.ContactID)
                      .HasName("PK_Contact_ContactID");
            });
        }
    }
}
