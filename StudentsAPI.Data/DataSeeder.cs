using Microsoft.AspNetCore.Identity;
using StudentsAPI.Data.Security;
using StudentsAPI.Data.Students;
using System.Collections.Generic;
using System.Linq;

namespace StudentsAPI.Data
{
    public class DataSeeder
    {
        public static void SeedData(DataBaseContext context, UserManager<User> userManager, RoleManager<IdentityRole> rolManager)
        {            
            string[] roleNames = { "Admin" };
            foreach (var roleName in roleNames)
            {
                if (rolManager.FindByNameAsync(roleName).Result == null)
                {
                    rolManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            if (userManager.FindByEmailAsync("admin@xyz.com").Result == null)
            {
                var user = new User
                {
                    UserName = "admin@xyz.com",
                    Email = "admin@xyz.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "Adm123456.").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByEmailAsync("abc@xyz.com").Result == null)
            {               
                var user = new User
                {
                    UserName = "abc@xyz.com",
                    Email = "abc@xyz.com"
                };
                _ = userManager.CreateAsync(user, "Abc123456.").Result;
            }


            if (!context.Teachers.Any())
            {
                var teachers = new List<Teacher>() {
                    new Teacher { Id = 1, Name = "Yilmer Guativa" },
                    new Teacher { Id = 2, Name = "Camilo Ortiz" },
                    new Teacher { Id = 3, Name = "Sebastian Gomez" },
                    new Teacher { Id = 4, Name = "Ana Maria Rodriguez" },
                    new Teacher { Id = 5, Name = "Carolina Suarez" }
                };
                context.AddRange(teachers);
            }
            if (!context.Subjects.Any())
            {
                var subjects = new List<Subject>() {
                    new Subject { Id = 1, Name = "Fundamentos de Programación", Credits = 3, TeacherId = 5 },
                    new Subject { Id = 2, Name = ".NET ASP Core", Credits = 3, TeacherId = 1 },
                    new Subject { Id = 3, Name = "NodeJs", Credits = 3, TeacherId = 3 },
                    new Subject { Id = 4, Name = "GraphQL", Credits = 3, TeacherId = 2 },
                    new Subject { Id = 5, Name = "Bases de datos", Credits = 3, TeacherId = 4 },
                    new Subject { Id = 6, Name = "Diseño UI", Credits = 3, TeacherId = 4 },
                    new Subject { Id = 7, Name = "Machine Learning", Credits = 3, TeacherId = 5 },
                    new Subject { Id = 8, Name = "Angular y NGRX", Credits = 3, TeacherId = 1 },
                    new Subject { Id = 9, Name = "React y Redux", Credits = 3, TeacherId = 3 },
                    new Subject { Id = 10, Name = "Azure Cloud", Credits = 3, TeacherId = 2 }
                };
                context.AddRange(subjects);
            }
            if (!context.Students.Any())
            {
                var students = new List<Student>(){
                    new Student { Id = 1, Identification = "1234698", Name = "Santiago Lasso" },
                    new Student { Id = 2, Identification = "978989898", Name = "Tatiana Rincón" },
                    new Student { Id = 3, Identification = "564213133", Name = "Karen Sabogal" },
                    new Student { Id = 4, Identification = "778899988", Name = "Gabriela Rey" },
                    new Student { Id = 5, Identification = "457891551", Name = "Andres Luna" }
                };
                context.AddRange(students);

                var studentSubjects = new List<StudentSubject>(){
                   new StudentSubject { StudentId = 1, SubjectId = 10 },
                   new StudentSubject { StudentId = 1, SubjectId = 3 },
                   new StudentSubject { StudentId = 1, SubjectId = 1 },
                   new StudentSubject { StudentId = 2, SubjectId = 9 },
                   new StudentSubject { StudentId = 2, SubjectId = 1 },
                   new StudentSubject { StudentId = 2, SubjectId = 5 },
                   new StudentSubject { StudentId = 3, SubjectId = 6 },
                   new StudentSubject { StudentId = 3, SubjectId = 8 },
                   new StudentSubject { StudentId = 3, SubjectId = 9 },
                   new StudentSubject { StudentId = 4, SubjectId = 5 },
                   new StudentSubject { StudentId = 4, SubjectId = 8 },
                   new StudentSubject { StudentId = 4, SubjectId = 1 },
                   new StudentSubject { StudentId = 5, SubjectId = 7 },
                   new StudentSubject { StudentId = 5, SubjectId = 3 },
                   new StudentSubject { StudentId = 5, SubjectId = 2 }
                };
                context.AddRange(studentSubjects);
            }

            context.SaveChanges();
        }
    }
}


