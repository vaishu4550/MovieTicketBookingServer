using MongoDB.Driver;
using MovieTicketBooking.Data;
using MovieTicketBooking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Business.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IMongoCollection<Movie> _movie;

        public MovieRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _movie = database.GetCollection<Movie>("Movie");
        }

        public async Task<PrepareResponse> Create(Movie movie)
        {
            var response = new PrepareResponse();
            try
            {
                await _movie.InsertOneAsync(movie);
                response.IsSuccess = true;
                response.Message = "Movie created";
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<PrepareResponse> DeleteMovie(string movieId)
        {
            var response = new PrepareResponse();
            try
            {
                await _movie.DeleteOneAsync(m => m.Id == movieId);
                response.IsSuccess = true;
                response.Message = "Movie deleted";
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
        }

        public async Task<List<Movie>> GetMovie()
        {
            var movie = await _movie.FindAsync(m => true);
            return await movie.ToListAsync();
        }

        public async Task<Movie> GetMovie(string movieId)
        {
            var movie = await _movie.FindAsync(m => m.Id == movieId);
            return await movie.FirstOrDefaultAsync();
        }
    }
}
