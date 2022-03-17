using ModuloServicosClienteWorker.Infra.Servicos;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosClienteWorker.Workers
{
    public abstract class BaseWorker : BackgroundService
    {
        protected readonly IZeebeClient client;

        protected readonly ICamundaCloudWorkerFactory workerFactory;

        protected BaseWorker(IZeebeClient client, ICamundaCloudWorkerFactory workerFactory)
        {
            this.client = client;
            this.workerFactory = workerFactory;
        }
        protected abstract string JobType { get; }

        protected abstract string WorkerName { get; }

        protected abstract Task JobHandler(IJobClient jobClient, IJob activatedJob);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                workerFactory.CreateWorker(client, JobType, JobHandler, WorkerName);
            });
        }
    }
}
