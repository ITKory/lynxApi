﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class Crew
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int SearchDepartureId { get; set; }

    public bool IsActive { get; set; }

    public bool IsArrived { get; set; }

    public virtual ICollection<CrewMate> CrewMates { get; set; } = new List<CrewMate>();

    public virtual SearchDeparture SearchDeparture { get; set; } = null!;
}
