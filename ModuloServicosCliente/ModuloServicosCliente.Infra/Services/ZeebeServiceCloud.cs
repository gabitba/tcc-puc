using Microsoft.Extensions.Options;
using ModuloServicosCliente.Application.Interfaces;
using ModuloServicosCliente.Infra.Options;
using Zeebe.Client.Impl.Builder;

namespace ModuloServicosCliente.Infra.Services
{
    public class ZeebeServiceCloud : ZeebeServiceBase
    {
        public ZeebeServiceCloud(IOptions<CamundaCloudClientOptions> clientOptions, IOptions<CamundaCloudWorkerOptions> workerOptions)
        {
            this.workerOptions = workerOptions;

           client = CamundaCloudClientBuilder
               .Builder()
               .UseClientId(clientOptions.Value.ClientId)
               .UseClientSecret(clientOptions.Value.ClientSecret)
               .UseContactPoint(clientOptions.Value.ContactPoint)
               .UseAuthServer(clientOptions.Value.AuthServer)
               .Build();
        }
    }
}
