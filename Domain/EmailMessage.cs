namespace LudyCakeShop.Domain
{
    public abstract class EmailMessage
    {
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
