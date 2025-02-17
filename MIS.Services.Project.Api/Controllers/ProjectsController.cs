using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS.Services.Project.Api.DTOs;
using MIS.Services.Project.Api.Repository;
using MIS.Services.Project.Api.Models;

namespace MIS.Services.Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;


        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<IEnumerable<ProjectRequestDto>> GetProjects()
        {
            return await _projectRepository.GetAllAsync();

            //if (project == null)
            //{
            //    return (IEnumerable<ProjectRequestDto>)NotFound();
            //}
            //return (IEnumerable<ProjectRequestDto>)Ok(project);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProjectRequestDto>>> GetProject(int id)
        {

            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, MIS.Services.Project.Api.Models.Project project)
        {
            var result = await _projectRepository.PutProject(id, project);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MIS.Services.Project.Api.Models.Project>> PostProject(ProjectResponseDto project)
        {
            int projectId = await _projectRepository.AddProjectAsync(project);
            return CreatedAtAction("GetProject", new { id = projectId }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                await _projectRepository.DeleteProjectAsync(id);
            }
            catch (Exception ex)
            {
                if (ex.Message == "NotFound")
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }


    }
}
