using System;
using System.Collections.Generic;

namespace MIS.Services.Project.Api.Models;

public partial class Vertical
{
    public int VerticalId { get; set; }

    public string? VerticalName { get; set; }

    public virtual ICollection<Project> Projects { get; } = new List<Project>();
}
