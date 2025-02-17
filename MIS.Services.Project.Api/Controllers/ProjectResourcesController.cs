using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS.Services.Project.Api.DTOs;
using MIS.Services.Project.Api.Models;
using MIS.Services.Project.Api.Repository;

namespace MIS.Services.Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectResourcesController : ControllerBase
    {
        private readonly ProjectdbContext _context;
        private readonly IProjectResourcesRepository _projectResourcesRepository;

        public ProjectResourcesController(IProjectResourcesRepository projectResourcesRepository)
        {
            _projectResourcesRepository = projectResourcesRepository;
        }

        // GET: api/ProjectResources
        [HttpGet]
        public async Task<IEnumerable<ProjectResourcesRequestDto>> GetProjectResources()
        {
            var resources = await _projectResourcesRepository.GetAllAsync();

            return resources;
        }

        // GET: api/ProjectResources/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectResource>> GetProjectResource(int id)
        {
            var resources = await _projectResourcesRepository.GetByIdAsync(id);

            if (resources == null)
            {
                return NotFound();
            }
            return Ok(resources);
        }

        // PUT: api/ProjectResources/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectResource(int id, ProjectResource projectResource)
        {
            var res = await _projectResourcesRepository.PutResource(id, projectResource);
            if (!res)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/ProjectResources
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectResource>> PostProjectResource(ProjectResourcesResponseDto projectResource)
        {
            int resourceId = await _projectResourcesRepository.AddResourceAsync(projectResource);
            return CreatedAtAction("GetProjectResource", new { id = resourceId }, projectResource);
        }

        // DELETE: api/ProjectResources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectResource(int id)
        {
            try
            {
                await _projectResourcesRepository.DeleteResourceAsync(id);
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

        private bool ProjectResourceExists(int id)
        {
            return (_context.ProjectResources?.Any(e => e.ResourceId == id)).GetValueOrDefault();
        }
    }
}
