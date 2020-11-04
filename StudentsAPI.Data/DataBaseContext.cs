using System;
using Microsoft.EntityFrameworkCore;
using StudentsAPI.Data.Entities;

namespace StudentsAPI.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentSubject>()
                .HasKey(ab => new {
                    ab.StudentId,
                    ab.SubjectId
                });          
           
        }
    }
}
