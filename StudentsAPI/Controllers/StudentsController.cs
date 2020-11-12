using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Models;
using StudentsAPI.Services.Interfaces;

namespace StudentsAPI.Controllers
{
    /// <summary>
    /// Controller for endpoints the actions Student
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService StudentService;
        /// <summary>
        /// *Controller for endpoints the actions Student
        /// </summary>
        /// <param name="service"></param>
        public StudentsController(IStudentService service)
        {
            StudentService = service;
        }

        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns>Enumerable of StudentModel</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentModel>>> GetStudents()
        {
            try
            {             
                return Ok(await StudentService.GetStudents());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Get student by id
        /// </summary>
        /// <param name="id">Id of register</param>
        /// <returns>StudentModel</returns>
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

                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        /// <summary>
        /// Create a new register of student 
        /// </summary>
        /// <param name="student">StudentModel</param>
        /// <returns>Id of register</returns>
        [HttpPost]
        public async Task<ActionResult<int>> PostStudent([FromBody] StudentModel student)
        {
            try
            {
                return Ok(await StudentService.CreateStudent(student));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Update a student
        /// </summary>
        /// <param name="id">Id of register for update</param>
        /// <param name="student">StudentModel</param>
        /// <returns>Id of register</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, [FromBody] StudentModel student)
        {
            try
            {
                if (id != student.Id)
                {
                    return BadRequest();
                }

                await StudentService.UpdateStudent(id, student);

                return Ok();
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
