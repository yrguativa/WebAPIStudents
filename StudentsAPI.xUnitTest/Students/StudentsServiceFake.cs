using StudentsAPI.Models;
using StudentsAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.xUnitTest.Students
{
    public class StudentServiceFake : IStudentService
    {
        private readonly List<StudentModel> Students;
        public StudentServiceFake()
        {
            Students = new List<StudentModel>()
            {
                new StudentModel() {
                    Id = 1,
                    Name = "Camilo Ramirez ",
                    Identification="789456"
                },
                new StudentModel() {
                    Id = 2,
                    Name = "Jorge Cano",
                    Identification="123456"
                },
                new StudentModel() {
                    Id = 3,
                    Name = "Fernando Herrera",
                    Identification="654321"
                }
            };
        }

        public Task<List<StudentModel>> GetStudents()
        {
            return Task.Run(() => Students);
        }

        public Task<StudentModel> GetStudent(int id)
        {
            return Task.Run(() => Students.Where(a => a.Id == id).FirstOrDefault());
        }

        public Task<int> CreateStudent(StudentModel model)
        {
            model.Id = 5;
            Students.Add(model);
            return Task.Run(() => model.Id);
        }

        public Task UpdateStudent(int id, StudentModel student)
        {
            var studentFound = Students.FirstOrDefault(s => s.Id == id);
            if (studentFound == null)            
            {
                throw new Exception("NotFound");
            }
            student.Id = 4;
            Students.Add(student);
            return Task.Run(() => { });
        }
    }
}
