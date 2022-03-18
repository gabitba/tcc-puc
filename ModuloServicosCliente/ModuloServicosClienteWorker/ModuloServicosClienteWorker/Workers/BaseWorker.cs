using ModuloServicosClienteWorker.Infra.Servicos;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosClienteWorker.Workers
{
    public abstract class BaseWorker : BackgroundService
    {
        protected readonly ICamundaCloudClientFactory clientFactory;
        protected readonly ICamundaCloudWorkerFactory workerFactory;

        protected readonly IJobWorker worker;

        protected BaseWorker(ICamundaCloudClientFactory clientFactory, ICamundaCloudWorkerFactory workerFactory)
        {
            this.clientFactory = clientFactory;
            this.workerFactory = workerFactory;
        }
        protected abstract string JobType { get; }

        protected abstract string WorkerName { get; }

        protected abstract Task JobHandler(IJobClient jobClient, IJob activatedJob);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                using(var client = clientFactory.CreateClient())
                {
                    using (var job = workerFactory.CreateWorker(client, JobType, JobHandler, WorkerName))
                    {
                        do
                        {
                        } while (!stoppingToken.IsCancellationRequested);
                    }
                }
            });
        }
    }
}
