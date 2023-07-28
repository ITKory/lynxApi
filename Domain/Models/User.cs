using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class User
{
    public int Id { get; set; }

    public int ProfileId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    [JsonIgnore]
    public virtual ICollection<CrewMate> CrewMates { get; set; } = new List<CrewMate>();
    [JsonIgnore]

    public virtual ICollection<FoundStat> FoundStats { get; set; } = new List<FoundStat>();

    public virtual Profile Profile { get; set; } = null!;
    [JsonIgnore]

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    [JsonIgnore]

    public virtual ICollection<SearchDeparture> SearchDepartureCartographers { get; set; } = new List<SearchDeparture>();
    [JsonIgnore]

    public virtual ICollection<SearchDeparture> SearchDepartureSearchAdministrators { get; set; } = new List<SearchDeparture>();
    [JsonIgnore]

    public virtual ICollection<SearchRequest> SearchRequests { get; set; } = new List<SearchRequest>();
    [JsonIgnore]

    public virtual ICollection<SeekerRegistration> SeekerRegistrations { get; set; } = new List<SeekerRegistration>();
}
