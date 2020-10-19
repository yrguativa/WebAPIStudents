using Microsoft.EntityFrameworkCore;
using StudentsAPI.Data;
using StudentsAPI.Data.Entities;
using StudentsAPI.Models;
using StudentsAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.Services.Implementacions
{
    public class SubjectService : ISubjectService
    {
        private readonly DataBaseContext _context;
        public SubjectService(DataBaseContext context)
        {
            _context = context;
        }
                
        public async Task<List<SubjectModel>> GetSubjects()
        {
            try
            {
                return await _context.Subjects
                                .Select(sb => new SubjectModel()
                                {
                                    Id = sb.Id,
                                    Name = sb.Name,
                                    Credits = sb.Credits,
                                    TeacherId = sb.TeacherId,
                                    Teacher = sb.Teacher == null ? null : new TeacherModel()
                                    {
                                        Id = sb.Teacher.Id,
                                        Name = sb.Teacher.Name
                                    },
                                    Students = sb.StudentsSubject == null ? null : sb.StudentsSubject.Select(ss => new StudentModel()
                                    {
                                        Id = ss.Student.Id,
                                        Identification = ss.Student.Identification,
                                        Name = ss.Student.Name,
                                    }).ToList()
                                })
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<SubjectModel> GetSubject(int id)
        {
            try
            {
                return await _context.Subjects
                    .Select(sb => new SubjectModel()
                    {
                        Id = sb.Id,
                        Name = sb.Name,
                        Credits = sb.Credits,
                        TeacherId = sb.TeacherId,
                        Teacher = sb.Teacher == null ? null : new TeacherModel()
                        {
                            Id = sb.Teacher.Id,
                            Name = sb.Teacher.Name
                        },
                        Students = sb.StudentsSubject == null ? null : sb.StudentsSubject.Select(ss => new StudentModel()
                        {
                            Id = ss.Student.Id,
                            Identification = ss.Student.Identification,
                            Name = ss.Student.Name,                            
                        }).ToList()                        
                    })
                    .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {

                throw ex;
            }           
        }

        public async Task<int> CreateSubject(SubjectModel model)
        {
            try
            {
                var subject = new Subject()
                {
                    Name = model.Name,
                    Credits = model.Credits,
                    TeacherId = model.TeacherId
                };
                _context.Subjects.Add(subject);
                await _context.SaveChangesAsync();

                return subject.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateSubject(int id, SubjectModel model)
        { 
            try
            {
                var subject = new Subject()
                {
                    Id = id,
                    Name = model.Name,
                    Credits = model.Credits,
                    TeacherId = model.TeacherId
                };              
               
                _context.Entry(subject).State = EntityState.Modified;
                
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }       
    }
}
