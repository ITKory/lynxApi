using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class City
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    [JsonIgnore]

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();
}
