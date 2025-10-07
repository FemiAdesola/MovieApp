namespace API.DTOs
{
    public class AuthenticationDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string? Email { get; set; }
    }
}