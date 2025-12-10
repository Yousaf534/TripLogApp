using System.Collections.Generic;

namespace TripLog.Models
{
    public class Accommodation
    {
        public int AccommodationId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}
