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
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PrepareResponse> Create(MovieDto movie)
        {
            var movieData = _mapper.Map<Movie>(movie);
            movieData.Created = DateTime.Now;
            movieData.Updated = DateTime.Now;

            return await _repository.Create(movieData);
        }

        public async Task<List<Movie>> GetMovie()
        {
            return await _repository.GetMovie();
        }

        public async Task<Movie> GetMovie(string movieId)
        {
            return await _repository.GetMovie(movieId);
        }

        public async Task<PrepareResponse> DeleteMovie(string movieId)
        {
            return await _repository.DeleteMovie(movieId);
        }
    }
}
