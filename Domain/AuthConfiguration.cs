namespace LudyCakeShop.Domain
{
    public class AuthConfiguration
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string SigningKeySecret { get; set; }
        public int TokenExpiration { get; set; }
    }
}
