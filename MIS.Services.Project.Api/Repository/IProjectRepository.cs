using Microsoft.AspNetCore.Mvc;
using MIS.Services.Project.Api.DTOs;
using MIS.Services.Project.Api.Models;

namespace MIS.Services.Project.Api.Repository
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectRequestDto>> GetAllAsync();
        Task<ProjectRequestDto> GetByIdAsync(int id);
        Task<bool> PutProject(int id, MIS.Services.Project.Api.Models.Project project);
        Task<int> AddProjectAsync(ProjectResponseDto project);
        Task DeleteProjectAsync(int id);
    }
}
