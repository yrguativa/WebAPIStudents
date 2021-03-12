using StudentsAPI.Models.Students;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentsAPI.Services.Students.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentModel>> GetStudents();
        Task<StudentModel> GetStudent(int id);
        Task<int> CreateStudent(StudentModel model);
        Task UpdateStudent(int id, StudentModel student);
    }
}
