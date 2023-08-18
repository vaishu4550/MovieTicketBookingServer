using MovieTicketBooking.Data.Models;
using MovieTicketBooking.Data.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Business.Service
{
    public interface IMovieService
    {
       
        Task<PrepareResponse> Create(MovieDto movie);

      
        Task<List<Movie>> GetMovie();
        
       
        Task<Movie> GetMovie(string movieId);

       
        Task<PrepareResponse> DeleteMovie(string movieId);
    }
}
