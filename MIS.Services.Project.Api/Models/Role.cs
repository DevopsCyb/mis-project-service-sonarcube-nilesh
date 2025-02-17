using System;
using System.Collections.Generic;

namespace MIS.Services.Project.Api.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<ProjectResource> ProjectResources { get; } = new List<ProjectResource>();
}
