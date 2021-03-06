using ModuloServicosCliente.Application.Interfaces;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosCliente.Workers
{
    public abstract class BaseWorker : BackgroundService
    {
        ILogger<BaseWorker> logger;
        protected readonly IZeebeService camundaService;

        protected BaseWorker(ILogger<BaseWorker> logger, IZeebeService camundaService)
        {
            this.logger = logger;
            this.camundaService = camundaService;
        }
        protected abstract string JobType { get; }

        protected abstract Task JobHandler(IJobClient jobClient, IJob activatedJob);

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (camundaService.CriarWorker(JobType, JobHandler, JobType))
            {
                logger.LogInformation("Started job: " + JobType);

                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, cancellationToken);
                }
            }
        }
    }
}
