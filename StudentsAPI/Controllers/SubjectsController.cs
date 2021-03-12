using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Models.Students;
using StudentsAPI.Services.Students.Interfaces;

namespace StudentsAPI.Controllers
{
    /// <summary>
    /// Controller for actions subjects
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                return Ok(await SubjectService.GetSubjects());
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

                return Ok(subjects);
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
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await SubjectService.CreateSubject(subject));
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
                    return BadRequest(id);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await SubjectService.UpdateSubject(id, subject);

                return NoContent();
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
        }
    }
}
