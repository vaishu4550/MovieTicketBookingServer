using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MovieTicketBooking.Data;
using MovieTicketBooking.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MovieTicketBooking.Business.Repository
{

    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseSettings _settings;
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<User> _user;


        public UserRepository(IDatabaseSettings settings, IConfiguration configuration)
        {
            _settings = settings;
            _configuration = configuration;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<User>("User");
        }


        public async Task<PrepareResponse> CreateUser(User data)
        {
            var response = new PrepareResponse();
            if (await _user.Find(u => u.Username == data.Username).AnyAsync())
            {
                response.IsSuccess = false;
                response.Message = "username already exists";
                return response;
            }
            try
            {
                await _user.InsertOneAsync(data);
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


        public async Task<bool> CheckUserExistsByUsername(string username)
        {
            return await _user.Find(u => u.Username == username).AnyAsync();
        }


        public async Task<User> GetUserByUsername(string username)
        {
            return await _user.Find(u => u.Username == username).FirstOrDefaultAsync();
        }


        public async Task<bool> VerifyUserPassword(string password, User user)
        {
            return VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }


        public string GenerateToken(User user, string role)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<PasswordUpdateResponse> ForgotPassword(ForgotPassword userPassword, User user)
        {
            var response = new PasswordUpdateResponse();


            if (userPassword.NewPassword == userPassword.ConfirmPassword)
            {
                CreatePasswordHash(userPassword.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _user.ReplaceOneAsync(u => u.Id == user.Id, user);

                response.status = true;
                response.message = "Password updated";
                return response;
            }
            else
            {
                response.status = false;
                response.message = "new password and confirm password not match";
                return response;
            }
        }

    }
}

