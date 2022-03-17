using Zeebe.Client;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosClienteWorker.Infra.Servicos
{
    public interface ICamundaCloudWorkerFactory
    {
        void CreateWorker(IZeebeClient client, string jobType, AsyncJobHandler jobHandler, string workerName);
    }
}
