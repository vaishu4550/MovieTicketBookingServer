using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieTicketBooking.Data.Models
{
    
    public class Theatre
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

       
        public string TheatreName { get; set; }

        
        public string City { get; set; }

       
        public int SeatCount { get; set; }

       
        public DateTime Created { get; set; }

        
        public DateTime Updated { get; set; }
    }

    
    public class TheatreDto
    {
        
        public string TheatreName { get; set; }

       
        public string City { get; set; }
    }
}
