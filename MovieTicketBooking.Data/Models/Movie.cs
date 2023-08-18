using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MovieTicketBooking.Data.Models
{
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string MovieName { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public string Languages { get; set; }
            
        public DateTime Created { get; set; }
        
        public DateTime Updated { get; set; }
    }
}
