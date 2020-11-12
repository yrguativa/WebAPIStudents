using StudentsAPI.Models;
using StudentsAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.xUnitTest.Students
{
    public class StudentServiceFake : IStudentService
    {
        private readonly List<StudentModel> students;
        public StudentServiceFake()
        {
            students = new List<StudentModel>()
            {
                new StudentModel() {
                    Id = 1,
                    Name = "Orange Juice",
                    Identification="789456"
                },
                new StudentModel() {
                    Id = 2,
                    Name = "Diary Milk",
                    Identification="123456"
                },
                new StudentModel() {
                    Id = 3,
                    Name = "Frozen Pizza",
                    Identification="654321"
                }
            };
        }

        public Task<List<StudentModel>> GetStudents()
        {
            return Task.Run(() => students);
        }

        public Task<StudentModel> GetStudent(int id)
        {
            return Task.Run(() => students.Where(a => a.Id == id).FirstOrDefault());
        }

        public Task<int> CreateStudent(StudentModel model)
        {
            model.Id = 4;
            students.Add(model);
            return Task.Run(() => model.Id);
        }

        public Task UpdateStudent(int id, StudentModel student)
        {
            student.Id = 4;
            students.Add(student);
            return Task.Run(() => { });
        }
    }
}
