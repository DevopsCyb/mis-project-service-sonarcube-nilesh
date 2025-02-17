using MIS.Services.Project.Api.Models;

namespace MIS.Services.Project.Api.DTOs
{
    public class ProjectResponseDto
    {
        public ProjectResponseDto() { }

        public int? CustomerId { get; set; }

        public string? ProjectName { get; set; }

        public int? ManagerId { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? StartDate { get; set; }


        public int verticalId { get; set; }
    }
}
