using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class SeekerRegistrationRequestModel
    {
        public int DepartureId { get; set; }
        public int UserId { get; set; }
        public TimeOnly StartTime { get; set; }
    }
}
