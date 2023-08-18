using AutoMapper;
using MovieTicketBooking.Business.Repository;
using MovieTicketBooking.Data.Models;
using System.Security.Cryptography;

namespace MovieTicketBooking.Business.Service
{
    public class UserService : IUserService
    {
        public UserService(IUserRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

       
        public readonly IUserRepository Repository;

        
        public readonly IMapper Mapper;

        
        public async Task<PrepareResponse> CreateUser(UserDto data, bool isAdmin = false)
        {
            var newUser = Mapper.Map<User>(data);
            CreatePasswordHash(data.Password, out byte[] passwordHash, out byte[] passwordSalt);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            newUser.IsAdmin = isAdmin;
            newUser.Created = DateTime.Now;
            newUser.Updated = DateTime.Now;

            return await Repository.CreateUser(newUser);
        }

        public async Task<PasswordUpdateResponse> ForgotPassword(ForgotPassword userPassword, User user)
        {
            return await Repository.ForgotPassword(userPassword, user);
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
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
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
    }
}
