using System.Collections.Generic;

namespace StudentsAPI.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }

        public List<SubjectModel> Subjects { get; set; }
    }
}
