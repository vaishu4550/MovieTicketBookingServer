using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieTicketBooking.Data;
using MongoDB.Driver;

namespace MovieTicketBooking.Business.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IDatabaseSettings _settings;
        private readonly IMongoCollection<Tickets> _ticket;
        private readonly IMongoCollection<Theatre> _theatre;

        public TicketRepository(IDatabaseSettings settings)
        {
            _settings = settings;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _ticket = database.GetCollection<Tickets>("Ticket");
            _theatre = database.GetCollection<Theatre>("Theatre");
        }
        public async Task<PrepareResponse> TicketBook(Tickets ticket, string theatreId)
        {
            var response = new PrepareResponse();
            try
            {
                var theatre = await _theatre.Find(t => t.Id == theatreId).FirstOrDefaultAsync();
                if (ticket.TicketsCount > theatre.SeatCount)
                {
                    response.IsSuccess = true;
                    response.Message = $"Only {theatre.SeatCount} seats are available";
                    return response;
                }

                await _ticket.InsertOneAsync(ticket);

                theatre.SeatCount -= ticket.TicketsCount;
                theatre.Updated = DateTime.Now;

                await _theatre.ReplaceOneAsync(t => t.Id == theatreId, theatre);

                response.IsSuccess = true;
                response.Message = "Ticket Booked";
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
