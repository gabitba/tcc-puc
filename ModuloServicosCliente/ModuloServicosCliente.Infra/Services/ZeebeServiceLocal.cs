using Microsoft.Extensions.Options;
using ModuloServicosCliente.Infra.Options;
using Zeebe.Client;

namespace ModuloServicosCliente.Infra.Services
{
    public class ZeebeServiceLocal : ZeebeServiceBase
    {
        public ZeebeServiceLocal(IOptions<CamundaCloudClientOptions> clientOptions, IOptions<CamundaCloudWorkerOptions> workerOptions)
        {
            this.workerOptions = workerOptions;

            client = ZeebeClient
                .Builder()
                .UseGatewayAddress(clientOptions.Value.ContactPoint)
                .UsePlainText()
                .Build();
        }
    }
}
