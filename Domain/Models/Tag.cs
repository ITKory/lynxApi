using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class Tag
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    [JsonIgnore] 
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
