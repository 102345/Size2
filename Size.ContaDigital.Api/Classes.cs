namespace Size.ContaDigital.Api
{
    public class User
    {
        public string UserID { get; set; }
        public string Password { get; set; }
    }

    public static class Roles
    {
        public const string ROLE_API_CONTADIGITAL = "Acesso-APIContaDigital";
    }

    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
