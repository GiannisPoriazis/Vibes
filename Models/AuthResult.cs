namespace Vibes.Models
{
    public class AuthResult
    {
        public bool IsError { get; set; }
        public string? UserName { get; set; }
        public System.Security.Claims.ClaimsPrincipal? User { get; set; }
        public string? Error { get; set; }
    }
}
