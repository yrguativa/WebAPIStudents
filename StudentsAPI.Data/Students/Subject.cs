using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsAPI.Data.Students
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }       

        [Required(ErrorMessage = "{0} es requerido")]
        [MaxLength(45, ErrorMessage = "{0} no puede tener más de {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        public byte Credits { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<StudentSubject> StudentsSubject { get; set; }
    }
}
