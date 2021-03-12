﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsAPI.Data.Students
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [MaxLength(15, ErrorMessage = "{0} no puede tener más de {1} caracteres")]
        public string Identification { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [MaxLength(45, ErrorMessage = "{0} no puede tener más de {1} caracteres")]
        public string Name { get; set; }

        public virtual ICollection<StudentSubject> SubjectsStudent { get; set; }

    }
}
