using Microsoft.EntityFrameworkCore;
using MIS.Services.Project.Api.DTOs;
using MIS.Services.Project.Api.Models;

namespace MIS.Services.Project.Api.Repository
{
    public class ProjectResourcesRepository : IProjectResourcesRepository
    {
        private ProjectdbContext _projectContext { get; set; }

        public ProjectResourcesRepository(ProjectdbContext projectContext)
        {
            _projectContext = projectContext;
        }

        public async Task<IEnumerable<ProjectResourcesRequestDto>> GetAllAsync()
        {
            var resources = await _projectContext.ProjectResources.ToListAsync();
            return _projectContext.ProjectResources
                .Include(tr => tr.Role)
                .Include(tr => tr.Project)
                .Select(tr => new ProjectResourcesRequestDto
                {
                    ResourceId = tr.ResourceId,
                    EmployeeId = tr.EmployeeId,
                    StartDate = tr.StartDate,
                    EndDate = tr.EndDate,
                    RoleName = tr.Role.RoleName,
                    ProjectName = tr.Project.ProjectName
                }).OrderBy(tr => tr.ResourceId).ToList();
        }

        public async Task<ProjectResourcesRequestDto> GetByIdAsync(int id)
        {
            var resources = await _projectContext.ProjectResources
                .Include(tr => tr.Role)
                .Include(tr => tr.Project)
                .FirstOrDefaultAsync(tr => tr.ResourceId == id);
            if (resources == null)
            {
                return null;
            }
            return new ProjectResourcesRequestDto
            {
                ResourceId = resources.ResourceId,
                EmployeeId = resources.EmployeeId,
                StartDate = resources.StartDate,
                EndDate = resources.EndDate,
                RoleName = resources.Role.RoleName,
                ProjectName = resources.Project.ProjectName
            };
        }

        public bool ProjectResourceExists(int id)
        {
            return (_projectContext.ProjectResources?.Any(e => e.ResourceId == id)).GetValueOrDefault();
        }

        public async Task<bool> PutResource(int id, ProjectResource resources)
        {
            var entity = await _projectContext.ProjectResources.FindAsync(id);
            if (entity == null)
                return false;
            entity.EmployeeId = resources.EmployeeId;
            entity.StartDate = resources.StartDate;
            entity.ProjectId = resources.ProjectId;
            entity.ResourceId = resources.ResourceId;
            entity.RoleId = resources.RoleId;

            _projectContext.ProjectResources.Update(entity);
            await _projectContext.SaveChangesAsync();

            return true;
        }

        public async Task<int> AddResourceAsync(ProjectResourcesResponseDto resource)
        {
            var maxResourceId = await _projectContext.ProjectResources
                .Select(r => (int?)r.ResourceId)
                .MaxAsync();
            // if maxResourceId is null, then set it to 0
            maxResourceId = maxResourceId ?? 0;

            var res1 = new ProjectResource
            {
                ResourceId = (int)(maxResourceId + 1),
                EmployeeId = resource.EmployeeId,
                EndDate = resource.EndDate,
                StartDate = resource.StartDate,
                //                ProjectId= resource.ProjectId
            };

            var projId = await _projectContext.Projects
                .Include(p => p.ProjectResources)
                .FirstOrDefaultAsync(p => p.ProjectId == resource.ProjectId);

            var role = await _projectContext.Roles
                .Include(r => r.ProjectResources)
                .FirstOrDefaultAsync(r => r.RoleId == resource.RoleId);

            res1.Project = projId;
            res1.Role = role;

            _projectContext.ProjectResources.Add(res1);
            await _projectContext.SaveChangesAsync();

            return res1.ResourceId;
        }

        public async Task DeleteResourceAsync(int id)
        {
            if (_projectContext.ProjectResources == null)
            {
                throw new ArgumentNullException(nameof(_projectContext.ProjectResources));
            }
            var project = await _projectContext.ProjectResources.FindAsync(id);
            if (project == null)
            {
                throw new Exception("NotFound");
            }

            _projectContext.ProjectResources.Remove(project);
            await _projectContext.SaveChangesAsync();
        }
    }
}
