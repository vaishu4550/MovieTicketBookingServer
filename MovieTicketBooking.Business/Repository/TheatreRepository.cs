using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MovieTicketBooking.Data;
using MovieTicketBooking.Data.Models;

namespace MovieTicketBooking.Business.Repository
{
   
    public class TheatreRepository : ITheatreRepository
    {
        private readonly IDatabaseSettings _settings;
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Theatre> _theatre;

        
        public TheatreRepository(IDatabaseSettings settings, IConfiguration configuration)
        {
            _settings = settings;
            _configuration = configuration;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _theatre = database.GetCollection<Theatre>("Theatre");
        }

       
        public async Task<List<Theatre>> GetTheatre()
        {
            var theatres = await _theatre.Find(t => true).ToListAsync();
            return theatres;
        }

       
        public async Task<Theatre> GetTheatre(string id)
        {
            var theatre = await _theatre.Find(t => t.Id == id).FirstOrDefaultAsync();
            return theatre;
        }

     
        public async Task<PrepareResponse> AddTheatre(Theatre data)
        {
            var response = new PrepareResponse();
            try
            {
                await _theatre.InsertOneAsync(data);
                response.IsSuccess = true;
                response.Message = "Data inserted";
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
        }
       
    }
}
