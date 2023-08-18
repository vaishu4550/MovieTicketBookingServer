using AutoMapper;
using MovieTicketBooking.Business.Repository;
using MovieTicketBooking.Data.Models;

namespace MovieTicketBooking.Business.Service
{
   
    public class TheatreService : ITheatreService
    {
        public readonly ITheatreRepository Repository;
        public readonly IMapper Mapper;

       
        public TheatreService(ITheatreRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public Task<List<Theatre>> GetTheatre()
        {
            return Repository.GetTheatre();
        }

      
        public Task<Theatre> GetTheatre(string id)
        {
            return Repository.GetTheatre(id);
        }

       
        public async Task<PrepareResponse> AddTheatre(TheatreDto data)
        {
            var theatre = Mapper.Map<Theatre>(data);
            theatre.Created = DateTime.Now;
            theatre.Updated = DateTime.Now;

            return await Repository.AddTheatre(theatre);
        }
    
    }
}
