﻿using Microsoft.EntityFrameworkCore;
using StudentsAPI.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsAPI.Services.Students.Interfaces;
using StudentsAPI.Models.Students;
using StudentsAPI.Data.Students;

namespace StudentsAPI.Services.Students.Implementacions
{
    public class StudentService : IStudentService
    {
        private readonly DataBaseContext _context;
        public StudentService(DataBaseContext context)
        {
            _context = context;
        }
                
        public async Task<List<StudentModel>> GetStudents()
        {
            try
            {
                return await _context.Students
                                .Select(s => new StudentModel()
                                {
                                    Id = s.Id,
                                    Identification = s.Identification,
                                    Name = s.Name,
                                    Subjects = s.SubjectsStudent == null ? null : s.SubjectsStudent.Select(ss => new SubjectModel()
                                    {
                                        Id = ss.Subject.Id,
                                        Name = ss.Subject.Name,
                                        Credits = ss.Subject.Credits,
                                        TeacherId = ss.Subject.TeacherId,
                                        Teacher = ss.Subject.Teacher == null ? null : new TeacherModel()
                                        {
                                            Id = ss.Subject.Teacher.Id,
                                            Name = ss.Subject.Teacher.Name
                                        }
                                    }).ToList()
                                })
                                .ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        
        public async Task<StudentModel> GetStudent(int id)
        {
            try
            {
                return await _context.Students
                    .Select(s => new StudentModel()
                    {
                        Id = s.Id,
                        Identification = s.Identification,
                        Name = s.Name,
                        Subjects = s.SubjectsStudent == null ? null : s.SubjectsStudent.Select(ss => new SubjectModel()
                        {
                            Id = ss.Subject.Id,
                            Name = ss.Subject.Name,
                            Credits = ss.Subject.Credits,
                            TeacherId = ss.Subject.TeacherId,
                            Teacher = ss.Subject.Teacher == null ? null : new TeacherModel()
                            {
                                Id = ss.Subject.Teacher.Id,
                                Name = ss.Subject.Teacher.Name
                            }
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch
            {
                throw;
            }           
        }

        public async Task<int> CreateStudent(StudentModel model)
        {
            try
            {
                var student = new Student()
                {
                    Identification = model.Identification,
                    Name = model.Name,
                };
                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                return student.Id;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateStudent(int id, StudentModel model)
        { 
            try
            {
                var student = new Student()
                {
                    Id = id,
                    Identification = model.Identification,
                    Name = model.Name,
                };
                if (model.Subjects != null)
                {
                    model.Subjects.ForEach(sb =>
                    {
                        student.SubjectsStudent.Add(new StudentSubject
                        {
                            StudentId = id,
                            SubjectId = sb.Id
                        });
                    });
                }
                _context.Entry(student).State = EntityState.Modified;
                
                await _context.SaveChangesAsync();
            }
            catch 
            {
                throw ;
            }            
        }       
    }
}
