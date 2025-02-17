namespace MIS.Services.Project.Api.DTOs
{
    public class ProjectResourcesResponseDto
    {
        public int ResourceId { get; set; }

        public int? EmployeeId { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? StartDate { get; set; }

        public int ProjectId { get; set; }

        public int RoleId { get; set; }
    }
}
