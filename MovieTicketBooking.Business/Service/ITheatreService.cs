using MovieTicketBooking.Data.Models;


namespace MovieTicketBooking.Business.Service
{
   
    public interface ITheatreService
    {
       
        Task<List<Theatre>> GetTheatre();

      
        Task<Theatre> GetTheatre(string id);

       
        Task<PrepareResponse> AddTheatre(TheatreDto data);

    }
}
