using System.Collections.Generic;

namespace StudentsAPI.Models.Students
{
    public class SubjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Credits { get; set; }
        public int TeacherId { get; set; }

        public  TeacherModel Teacher { get; set; }
        public  List<StudentModel> Students { get; set; }
    }
}
