namespace YaDea.Messaging.Identity.Options
{
    public class JwtSettingOptions
    {
        public string SecurityKey { get; set; }

        public TimeSpan ExpiresIn { get; set; }
    }
}