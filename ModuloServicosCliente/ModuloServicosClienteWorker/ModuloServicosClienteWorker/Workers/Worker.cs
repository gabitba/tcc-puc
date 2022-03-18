using ModuloServicosClienteWorker.Infra.Servicos;
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
            ICamundaCloudClientFactory clientFactory,
            ICamundaCloudWorkerFactory workerFactory) : base(clientFactory, workerFactory)
        {
            this.logger = logger;
        }

        protected override string JobType => "get-time";

        protected override string WorkerName => "get-time";

        protected override async Task JobHandler(IJobClient jobClient, IJob activatedJob)
        {
            //logger.LogInformation("Received job: " + activatedJob);

            Console.WriteLine($"Received job: {activatedJob}");
            await jobClient.NewCompleteJobCommand(activatedJob.Key).Send();
        }
    }
}