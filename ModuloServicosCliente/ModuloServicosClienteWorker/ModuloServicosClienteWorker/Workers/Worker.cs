using ModuloServicosClienteWorker.Infra.Servicos;
using Newtonsoft.Json;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosClienteWorker.Workers
{
    public class Worker : BaseWorker
    {
        private readonly ILogger<Worker> logger;

        public Worker(
            ILogger<Worker> logger,
            IZeebeClient client,
            ICamundaCloudWorkerFactory workerFactory) : base(client, workerFactory)
        {
            this.logger = logger;
        }

        protected override string JobType => "email";

        protected override string WorkerName => "CsharpGetStartedWorker";

        protected override async Task JobHandler(IJobClient jobClient, IJob activatedJob)
        {
            logger.LogInformation("Received job: " + activatedJob);

            await jobClient.NewCompleteJobCommand(activatedJob.Key).Send();
        }
    }
}