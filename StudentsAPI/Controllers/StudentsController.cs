using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Models;
using StudentsAPI.Services.Interfaces;

namespace StudentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentService StudentService;
        public StudentsController(IStudentService service)
        {
            StudentService = service;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentModel>>> GetStudents()
        {
            try
            {
                return await StudentService.GetStudents();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentModel>> GetStudent(int id)
        {
            try
            {
                var student = await StudentService.GetStudent(id);

                if (student == null)
                {
                    return NotFound();
                }

                return student;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        // POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<int>> PostStudent([FromBody] StudentModel student)
        {
            try
            {
                return await StudentService.CreateStudent(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, [FromBody] StudentModel student)
        {
            try
            {
                if (id != student.Id)
                {
                    return BadRequest();
                }

                await StudentService.UpdateStudent(id,student);
            }
            catch (Exception ex)
            {
                if (await StudentService.GetStudent(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex);
                }
            }

            return NoContent();

        }
    }
}
