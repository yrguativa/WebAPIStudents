using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsAPI.Data.Entities
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [MaxLength(45, ErrorMessage = "{0} no puede tener más de {1} caracteres")]
        public string Name { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
