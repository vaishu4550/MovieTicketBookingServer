using MovieTicketBooking.Data.Models;

namespace MovieTicketBooking.Business.Repository
{
    
    public interface ITheatreRepository
    {
      
        Task<List<Theatre>> GetTheatre();

        
        Task<Theatre> GetTheatre(string id);

        Task<PrepareResponse> AddTheatre(Theatre data);
       
    }
}
