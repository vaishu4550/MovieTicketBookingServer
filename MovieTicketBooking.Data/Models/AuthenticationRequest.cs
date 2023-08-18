namespace MovieTicketBooking.Data.Models
{
    public class AuthenticationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticationResponse
    {
        public string AccessToken { get; set; }
        public string UserId { get; set; }
    }
}
