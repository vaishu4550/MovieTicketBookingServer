using MovieTicketBooking.Data.Models;
using MovieTicketBooking.Data.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Business.Service
{
    public interface ITicketService
    {
        Task<PrepareResponse> TicketBook(TicketDto ticket, string userId, string theatreId);
    }
}
