using Microsoft.Extensions.Options;
using ModuloServicosClienteWorker.Infra.Options;
using Zeebe.Client;
using Zeebe.Client.Impl.Builder;

namespace ModuloServicosClienteWorker.Infra.Servicos
{
    public class CamundaCloudClientFactory : ICamundaCloudClientFactory
    {
        readonly IOptions<CamundaCloudClientOptions> options;
        public CamundaCloudClientFactory(IOptions<CamundaCloudClientOptions> options)
        {
            this.options = options;
        }

        public IZeebeClient CreateClient()
        {
            return CamundaCloudClientBuilder
                .Builder()
                .UseClientId(options.Value.ClientId)
                .UseClientSecret(options.Value.ClientSecret)
                .UseContactPoint(options.Value.ContactPoint)
                //.UseLoggerFactory(LoggerFactory) // optional
                .Build();
        }
    }
}
