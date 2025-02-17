using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS.Services.Project.Api.DTOs;
using MIS.Services.Project.Api.Models;
using System.Data;
using System.Text.Json;

namespace MIS.Services.Project.Api.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private ProjectdbContext _projectContext { get; set; }

        public ProjectRepository(ProjectdbContext projectContext)
        {
            _projectContext = projectContext;
        }

        public async Task<IEnumerable<ProjectRequestDto>> GetAllAsync()
        {
            //var projects = await _projectContext.Projects.ToListAsync();
            return await _projectContext.Projects
                .Include(tr => tr.Vertical)
                .Include(tr => tr.ProjectResources)
                .Select(tr => new ProjectRequestDto
                {
                    ProjectId = tr.ProjectId,
                    ProjectName = tr.ProjectName,
                    EndDate = tr.EndDate,
                    StartDate = tr.StartDate,
                    CustomerId = tr.CustomerId,
                    ManagerId = tr.ManagerId,
                    VerticalName = tr.Vertical.VerticalName,
                    ProjectResources = tr.ProjectResources.Select(s1 => new ProjectResourcesRequestDto()
                    {
                        EmployeeId = s1.EmployeeId,
                        StartDate = s1.StartDate,
                        EndDate = s1.EndDate,
                        ResourceId = s1.ResourceId,
                        RoleName = s1.Role.RoleName
                    }).ToList()
                }).ToListAsync();

        }

        public async Task<ProjectRequestDto> GetByIdAsync(int id)
        {
            var project = await _projectContext.Projects
                .Include(tr => tr.Vertical)
                .Include(tr => tr.ProjectResources)
                .FirstOrDefaultAsync(tr => tr.ProjectId == id);
            if (project == null)
            {
                return null;
            }
            return new ProjectRequestDto
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                CustomerId = project.CustomerId,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                ManagerId = project.ManagerId,
                VerticalName = project.Vertical?.VerticalName,
                ProjectResources = project.ProjectResources?.Select(s1 => new ProjectResourcesRequestDto()
                {
                    EmployeeId = s1.EmployeeId,
                    StartDate = s1.StartDate,
                    EndDate = s1.EndDate,
                    ResourceId = s1.ResourceId,
                    RoleName = s1.Role?.RoleName
                }).OrderBy(s1 => s1.ResourceId).ToList()
            };
        }

        public async Task<bool> PutProject(int id, MIS.Services.Project.Api.Models.Project project)
        {
            var entity = await _projectContext.Projects.FindAsync(id);
            if (entity == null)
                return false;
            entity.ProjectId = project.ProjectId;
            entity.ProjectName = project.ProjectName;
            entity.CustomerId = project.CustomerId;
            entity.ManagerId = project.ManagerId;
            _projectContext.Projects.Update(entity);
            await _projectContext.SaveChangesAsync();
            return true;
        }

        public async Task<int> AddProjectAsync(ProjectResponseDto project)
        {
            var maxProjectId = await _projectContext.Projects.MaxAsync(p => p.ProjectId);
            var Project1 = new MIS.Services.Project.Api.Models.Project
            {
                ProjectId = maxProjectId + 1,
                CustomerId = project.CustomerId,
                ProjectName = project.ProjectName,
                ManagerId = project.ManagerId,
                EndDate = project.EndDate,
                StartDate = project.StartDate,
            };

            var vertical = await _projectContext.Verticals
                .Include(v => v.Projects)
                .FirstOrDefaultAsync(v => v.VerticalId == project.verticalId);

            Project1.Vertical = vertical;

            //if (_projectContext.Projects == null)
            //{
            //    throw new ArgumentNullException(nameof(_projectContext.Projects));
            //}
            _projectContext.Projects.Add(Project1);

            await _projectContext.SaveChangesAsync();

            //var eventProject = new ProjectResponseDto
            //{
            //    CustomerId = Project1.CustomerId,
            //    ProjectName = Project1.ProjectName,
            //    verticalId = Project1.Vertical.VerticalId
            //};

            //// Publish event to Azure Service Bus
            //await _eventPublisher.PublishProjectAddedEventAsync(eventProject);
            return Project1.ProjectId;
        }

        private bool ProjectExists(int projectId)
        {
            return (_projectContext.Projects?.Any(e => e.ProjectId == projectId)).GetValueOrDefault();
        }

        public async Task DeleteProjectAsync(int id)
        {
            if (_projectContext.Projects == null)
            {
                throw new ArgumentNullException(nameof(_projectContext.Projects));
            }
            var project = await _projectContext.Projects.FindAsync(id);
            if (project == null)
            {
                throw new Exception("NotFound");
            }

            _projectContext.Projects.Remove(project);
            await _projectContext.SaveChangesAsync();
        }


    }
}
