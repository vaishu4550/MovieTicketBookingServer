using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Data.Models.Dto
{
    public class TicketDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int TicketsCount { get; set; }


        // <summary>
        /// 
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string MovieId { get; set; }
        public string TheatreId { get; set; }
    }
}
