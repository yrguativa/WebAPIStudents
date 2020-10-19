using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsAPI.Data.Entities
{
    public class StudentSubject
    { 
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }

        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
