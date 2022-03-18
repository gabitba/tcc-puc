namespace ModuloServicosClienteWorker.Infra.Options
{
    public class CamundaCloudClientOptions
    {
        public const string ConfigName = "CamundaCloudClient";

        public string ClientId { get; set; } = string.Empty;

        public string ClientSecret { get; set; } = string.Empty;

        public string ContactPoint { get; set; } = string.Empty;

        public string AuthServer { get; set; } = string.Empty;
    }
}
