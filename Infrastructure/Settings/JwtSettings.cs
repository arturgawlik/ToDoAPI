namespace Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public long ExpiryMinutes { get; set; }
    }
}