namespace ModuloEmail.API
{
    public class EmailOptions
    {
        public const string ConfigName = "Email";
        public string DestinatarioDefault { get; set; }
        public string BaseUrl { get; set; }
    }
}