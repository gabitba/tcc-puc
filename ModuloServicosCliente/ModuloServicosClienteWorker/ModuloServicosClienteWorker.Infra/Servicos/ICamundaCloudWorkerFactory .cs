using Zeebe.Client;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosClienteWorker.Infra.Servicos
{
    public interface ICamundaCloudWorkerFactory
    {
        IJobWorker CreateWorker(IZeebeClient client, string jobType, AsyncJobHandler jobHandler, string workerName);
    }
}
