using System;
using System.Collections.Generic;

namespace TripLog.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int DestinationId { get; set; }
        public Destination Destination { get; set; }

        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }

        public ICollection<TripActivity> TripActivities { get; set; } = new List<TripActivity>();
    }
}
