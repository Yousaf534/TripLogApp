using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TripLog.ViewModels
{
    public class TripStep1ViewModel
    {
        [Required]
        [Display(Name = "Destination")]
        public int DestinationId { get; set; }

        [Required]
        [Display(Name = "Accommodation")]
        public int AccommodationId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public IEnumerable<SelectListItem> Destinations { get; set; }
        public IEnumerable<SelectListItem> Accommodations { get; set; }
    }
}
