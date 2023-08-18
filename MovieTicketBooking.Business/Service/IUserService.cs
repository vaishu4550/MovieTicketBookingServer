using MovieTicketBooking.Data.Models;

namespace MovieTicketBooking.Business.Service
{
    public interface IUserService
    {
       
        Task<PrepareResponse> CreateUser(UserDto data, bool isAdmin = false);

        Task<PasswordUpdateResponse> ForgotPassword(ForgotPassword userPassword, User user);
    }
}
