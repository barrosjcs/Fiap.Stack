namespace Fiap.Stack.Security
{
    public class TokenSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Hours { get; set; }
        public string Key { get; set; }
    }
}
