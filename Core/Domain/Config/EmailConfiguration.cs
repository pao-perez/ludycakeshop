namespace LudyCakeShop.Core.Domain.Config
{
    public class EmailConfiguration
    {
        public string FromDisplay { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
