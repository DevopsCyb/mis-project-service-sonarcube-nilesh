using MIS.Services.Project.Api.Models;

namespace MIS.Services.Project.Api.DTOs
{
    public class ProjectResourcesRequestDto
    {
        public ProjectResourcesRequestDto()
        {
        }
        public int ResourceId { get; set; }

        public int? EmployeeId { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? StartDate { get; set; }

        public string? RoleName { get; set; }

        public string ProjectName { get; set; }
    }
}
