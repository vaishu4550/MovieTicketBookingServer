using AutoMapper;
using MovieTicketBooking.Business.Repository;
using MovieTicketBooking.Data.Models;
using MovieTicketBooking.Data.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Business.Service
{
    public class TicketService : ITicketService
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _repository;

        public TicketService(IMapper mapper, ITicketRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PrepareResponse> TicketBook(TicketDto ticket, string userId, string theatreId)
        {
            var ticketBook = _mapper.Map<Tickets>(ticket);
            ticketBook.UserId = userId;

            return await _repository.TicketBook(ticketBook, theatreId);
        }
    }
}
