using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Data.Models
{
    public class Tickets
    {
       
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TicketId { get; set; }

       
        public int TicketsCount { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        public string MovieId { get; set; }

       
        [BsonIgnore]
        public Movie Movie { get; set; }

       
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

     
        [BsonIgnore]
        public User User { get; set; }
    }
}
