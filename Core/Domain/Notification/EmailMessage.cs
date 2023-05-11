namespace LudyCakeShop.Core.Domain.Notification
{
    public abstract class EmailMessage
    {
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
