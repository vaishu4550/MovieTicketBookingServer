using MovieTicketBooking.Data.Models.Dto;
using MovieTicketBooking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Business.Repository
{
    public interface IMovieRepository
    {
       
        Task<PrepareResponse> Create(Movie movie);

       
        Task<List<Movie>> GetMovie();

       
        Task<Movie> GetMovie(string movieId);

       
        Task<PrepareResponse> DeleteMovie(string movieId);
    }
}
