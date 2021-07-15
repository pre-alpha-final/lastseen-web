using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LastSeenWeb.Data.Models
{
	public class NotesEntity
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public string OwnerName { get; set; }
		public string Notes { get; set; }
	}
}
