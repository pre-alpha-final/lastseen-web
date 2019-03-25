namespace LastSeenWeb.AngularFront.Controllers.Models
{
	public class LastSeenItemModel
	{
		// Data section
		public string Id { get; set; }

		// Status section
		public int Season { get; set; }
		public int Episode { get; set; }
		public string VisitUrl { get; set; }
		public bool Unfinished { get; set; }
		public int Hours { get; set; }
		public int Minutes { get; set; }
		public int Seconds { get; set; }
		public string Notes { get; set; }
		public bool MoveToTop { get; set; }

		// Config section
		public string Name { get; set; }
		public string ImageUrl { get; set; }

		// Tracking section
		public string TrackingUrl { get; set; }
		public int EpisodesBehind { get; set; }
	}
}
