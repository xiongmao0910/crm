namespace locy_api.Models.Requests
{
    public class LogInRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
