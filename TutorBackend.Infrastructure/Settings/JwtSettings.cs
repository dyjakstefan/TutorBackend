namespace TutorBackend.Infrastructure.Settings
{
    public class JwtSettings
    {
        public const string Jwt = "Jwt";

        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
