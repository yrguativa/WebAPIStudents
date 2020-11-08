using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Models;
using StudentsAPI.Services.Interfaces;

namespace StudentsAPI.Controllers
{
    /// <summary>
    /// Controller for actions subjects
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {

        private readonly ISubjectService SubjectService;
        public SubjectsController(ISubjectService service)
        {
            SubjectService = service;
        }

        /// <summary>
        /// Get all subjects
        /// </summary>
        /// <returns>Enumerable of SubjectModel</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectModel>>> GetSubjects()
        {
            try
            {
                return await SubjectService.GetSubjects();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Get subject by id
        /// </summary>
        /// <param name="id">Id of register</param>
        /// <returns>SubjectModel</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectModel>> GetSubject(int id)
        {
            try
            {
                var subjects = await SubjectService.GetSubject(id);

                if (subjects == null)
                {
                    return NotFound();
                }

                return subjects;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        /// <summary>
        /// Create a new register of subject 
        /// </summary>
        /// <param name="subject">SubjectModel</param>
        /// <returns>Id of register</returns>   
        [HttpPost]
        public async Task<ActionResult<int>> PostSubject([FromBody] SubjectModel subject)
        {
            try
            {
                return await SubjectService.CreateSubject(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Update a subject
        /// </summary>
        /// <param name="id">Id of register for update</param>
        /// <param name="subject">SubjectModel</param>
        /// <returns>Id of register</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, [FromBody] SubjectModel subject)
        {
            try
            {
                if (id != subject.Id)
                {
                    return BadRequest();
                }

                await SubjectService.UpdateSubject(id, subject);
            }
            catch (Exception ex)
            {
                if (await SubjectService.GetSubject(id) == null)
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
