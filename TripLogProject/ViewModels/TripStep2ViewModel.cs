using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TripLog.ViewModels
{
    public class TripStep2ViewModel
    {
        public int[] SelectedActivityIds { get; set; } = new int[0];
        public IEnumerable<SelectListItem> Activities { get; set; }
    }
}
