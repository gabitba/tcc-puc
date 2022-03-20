using ModuloServicosClienteWorker.Infra.Services;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosClienteWorker.Workers
{
    public class Worker : BaseWorker
    {
        private readonly ILogger<Worker> logger;

        public Worker(
            ILogger<Worker> logger,
            ICamundaService camundaService) : base(logger, camundaService)
        {
            this.logger = logger;
        }

        protected override string JobType => "get-time";

        protected override string WorkerName => "get-time";

        protected override async Task JobHandler(IJobClient jobClient, IJob activatedJob)
        {
            logger.LogInformation("Received job: " + activatedJob);
            await jobClient.NewCompleteJobCommand(activatedJob.Key).Send();
        }
    }
}