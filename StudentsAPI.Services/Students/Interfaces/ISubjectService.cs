using StudentsAPI.Models.Students;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentsAPI.Services.Students.Interfaces
{
    public interface ISubjectService
    {
        Task<List<SubjectModel>> GetSubjects();
        Task<SubjectModel> GetSubject(int id);
        Task<int> CreateSubject(SubjectModel model);
        Task UpdateSubject(int id, SubjectModel model);
    }
}
