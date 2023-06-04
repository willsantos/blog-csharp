namespace Blog
{
    public static class Configuration
    {
        public static string JwtKey = "fedaf7d8863b48e197b9287d492b708e";
        public static string ApiKeyName = "api_key";
        public static string ApiKeyValue= "blog_api_=daf7d8863b48e197b9287d492b";
        public static SmtpConfiguration Smtp = new();
    }

    public class SmtpConfiguration
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
