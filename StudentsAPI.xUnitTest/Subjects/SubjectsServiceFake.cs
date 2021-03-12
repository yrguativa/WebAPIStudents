using StudentsAPI.Models.Students;
using StudentsAPI.Services.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.xUnitTest.Subjects
{
    public class SubjectServiceFake : ISubjectService
    {
        private readonly List<SubjectModel> Subjects;
        public SubjectServiceFake()
        {
            Subjects = new List<SubjectModel>()
            {
                new SubjectModel() {
                    Id = 1,
                    Name = "ML (Machine Learning)",
                    Credits = 3,
                    TeacherId =1
                },
                new SubjectModel() {
                    Id = 2,
                    Name = "Angular",
                    Credits = 3,
                    TeacherId =1
                },
                new SubjectModel() {
                    Id = 3,
                    Name = "ASP Core",
                    Credits = 3,
                    TeacherId =2
                }
            };
        }

        public Task<List<SubjectModel>> GetSubjects()
        {
            return Task.Run(() => Subjects);
        }

        public Task<SubjectModel> GetSubject(int id)
        {
            return Task.Run(() => Subjects.Where(a => a.Id == id).FirstOrDefault());
        }

        public Task<int> CreateSubject(SubjectModel model)
        {
            model.Id = 5;
            Subjects.Add(model);
            return Task.Run(() => model.Id);
        }

        public Task UpdateSubject(int id, SubjectModel student)
        {
            var studentFound = Subjects.FirstOrDefault(s => s.Id == id);
            if (studentFound == null)            
            {
                throw new Exception("NotFound");
            }
            student.Id = 4;
            Subjects.Add(student);
            return Task.Run(() => { });
        }
    }
}
