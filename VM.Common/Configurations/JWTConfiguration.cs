namespace VM.Common.Configurations
{
    public class JWTConfiguration
    {
        public static string SettingName = "JWT";
        public string ValidAudience { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public int AccessTokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationMinutes { get; set; }
    }
}
