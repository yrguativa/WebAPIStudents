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
    public class SubjectsController : ControllerBase
    {

        private readonly ISubjectService SubjectService;
        public SubjectsController(ISubjectService service)
        {
            SubjectService = service;
        }

        // GET: api/Subjects
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

        // GET: api/Subjects/5
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

        // POST: api/Subjects       
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

        // PUT: api/Subjects
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
