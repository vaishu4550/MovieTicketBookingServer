using MovieTicketBooking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Business.Repository
{
    public interface ITicketRepository
    {
        Task<PrepareResponse> TicketBook(Tickets ticket, string theatreId);
    }
}
