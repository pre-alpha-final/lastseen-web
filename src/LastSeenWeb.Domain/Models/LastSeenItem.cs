using System;

namespace LastSeenWeb.Domain.Models
{
	public class LastSeenItem
	{
		// Data section
		public string Id { get; set; }
		public string Owner { get; set; }
		public DateTime Modified { get; set; }

		// Status section
		public int Season { get; set; }
		public int Episode { get; set; }
		public bool Unfinished { get; set; }
		public int Hours { get; set; }
		public int Minutes { get; set; }
		public int Seconds { get; set; }
		public string VisitUrl { get; set; }

		// Config section
		public string Name { get; set; }
		public string ImageUrl { get; set; }

		// Update section
		public string TrackingUrl { get; set; }
		public int EpisodesBehind { get; set; }
	}
}
