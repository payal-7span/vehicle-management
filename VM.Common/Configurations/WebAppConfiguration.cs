namespace VM.Common.Configurations
{
    public class WebAppConfiguration
    {
        public static string SettingName = "WebApp";
        public string Url { get; set; } = string.Empty;
        public List<string> OtherUrls { get; set; } = [];
    }
}
