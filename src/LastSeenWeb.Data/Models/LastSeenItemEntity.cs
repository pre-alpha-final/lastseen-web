using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LastSeenWeb.Data.Models
{
	public class LastSeenItemEntity
	{
		// Data section
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public string OwnerName { get; set; }
		public DateTime Modified { get; set; }

		// Status section
		public int Season { get; set; }
		public int Episode { get; set; }
		public bool Unfinished { get; set; }
		public int Hours { get; set; }
		public int Minutes { get; set; }
		public int Seconds { get; set; }
		public string VisitUrl { get; set; }
		public string Notes { get; set; }

		// Config section
		public string Name { get; set; }
		public string ImageUrl { get; set; }

		// Update section
		public string TrackingUrl { get; set; }
		public int EpisodesBehind { get; set; }

		// Removal
		public bool Remove { get; set; }
	}
}
