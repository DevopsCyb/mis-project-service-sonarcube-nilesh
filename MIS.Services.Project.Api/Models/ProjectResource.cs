using System;
using System.Collections.Generic;

namespace MIS.Services.Project.Api.Models;

public partial class ProjectResource
{
    public int ResourceId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? StartDate { get; set; }

    public int ProjectId { get; set; }

    public int RoleId { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
