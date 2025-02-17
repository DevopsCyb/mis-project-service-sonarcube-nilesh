using Castle.Components.DictionaryAdapter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MIS.Services.Project.Api.Models;

public partial class Project
{

    public int ProjectId { get; set; }

    public int? CustomerId { get; set; }

    public string? ProjectName { get; set; }

    public int? ManagerId { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? StartDate { get; set; }

    public int VerticalId { get; set; }

    public virtual ICollection<ProjectResource> ProjectResources { get; } = new List<ProjectResource>();

    public virtual Vertical Vertical { get; set; } = null!;
}
