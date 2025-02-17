using MIS.Services.Project.Api.DTOs;
using MIS.Services.Project.Api.Models;

namespace MIS.Services.Project.Api.Repository
{
    public interface IProjectResourcesRepository
    {
        Task<IEnumerable<ProjectResourcesRequestDto>> GetAllAsync();
        Task<ProjectResourcesRequestDto> GetByIdAsync(int id);
        Task<bool> PutResource(int id, ProjectResource resources);
        Task<int> AddResourceAsync(ProjectResourcesResponseDto resource);
        Task DeleteResourceAsync(int id);
        bool ProjectResourceExists(int id);
    }
}
