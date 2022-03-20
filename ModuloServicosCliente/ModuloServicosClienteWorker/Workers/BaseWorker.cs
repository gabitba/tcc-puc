using ModuloServicosClienteWorker.Infra.Services;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosClienteWorker.Workers
{
    public abstract class BaseWorker : BackgroundService
    {
        ILogger<BaseWorker> logger;
        protected readonly ICamundaService camundaService;

        protected BaseWorker(ILogger<BaseWorker> logger, ICamundaService camundaService)
        {
            this.logger = logger;
            this.camundaService = camundaService;
        }
        protected abstract string JobType { get; }

        protected abstract string WorkerName { get; }

        protected abstract Task JobHandler(IJobClient jobClient, IJob activatedJob);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                using (var job = camundaService.CriarWorker(JobType, JobHandler, WorkerName))
                {
                    logger.LogInformation("Started job: " + JobType);
                    do
                    {
                    } while (!stoppingToken.IsCancellationRequested);
                }
            });
        }
    }
}
