using MIS.Services.Project.Api.Models;
using System.Text.Json.Serialization;

namespace MIS.Services.Project.Api.DTOs
{
    public class ProjectRequestDto
    {
        public ProjectRequestDto()
        {
            ProjectResources = new HashSet<ProjectResourcesRequestDto>();
        }



        public int ProjectId { get; set; }

        public int? CustomerId { get; set; }

        public string? ProjectName { get; set; }

        public int? ManagerId { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? StartDate { get; set; }

        public string? VerticalName { get; set; } = null!;
        public virtual ICollection<ProjectResourcesRequestDto> ProjectResources { get; set; }
    }
}
