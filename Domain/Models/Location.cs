using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class Location
{
    public int Id { get; set; }

    public string Latitude { get; set; } = null!;

    public string Longitude { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();
    [JsonIgnore]
    public virtual ICollection<SearchDeparture> SearchDepartures { get; set; } = new List<SearchDeparture>();
    [JsonIgnore]
    public virtual ICollection<SearchRequest> SearchRequests { get; set; } = new List<SearchRequest>();
}
