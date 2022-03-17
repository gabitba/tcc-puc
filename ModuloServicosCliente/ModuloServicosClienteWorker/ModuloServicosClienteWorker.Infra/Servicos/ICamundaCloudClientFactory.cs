using Zeebe.Client;

namespace ModuloServicosClienteWorker.Infra.Servicos
{
    public interface ICamundaCloudClientFactory
    {
        IZeebeClient CreateClient();
    }
}
