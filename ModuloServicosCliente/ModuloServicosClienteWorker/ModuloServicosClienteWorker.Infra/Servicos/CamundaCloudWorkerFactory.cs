using Microsoft.Extensions.Options;
using ModuloServicosClienteWorker.Infra.Options;
using Zeebe.Client;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosClienteWorker.Infra.Servicos
{
    public class CamundaCloudWorkerFactory : ICamundaCloudWorkerFactory
    {
        readonly IOptions<CamundaCloudWorkerOptions> options;
        public CamundaCloudWorkerFactory(IOptions<CamundaCloudWorkerOptions> options)
        {
            this.options = options;
        }

        public IJobWorker CreateWorker(IZeebeClient client, string jobType, AsyncJobHandler jobHandler, string workerName)
        {
            
            return client.NewWorker()
                .JobType(jobType)
                .Handler(jobHandler)
                .MaxJobsActive(options.Value.MaxJobActive)
                .Timeout(TimeSpan.FromSeconds(options.Value.Timeout))
                .PollInterval(TimeSpan.FromSeconds(options.Value.PollInterval))
                .PollingTimeout(TimeSpan.FromSeconds(options.Value.PollingTimeout))
                .Name(workerName)
                .Open();
        }
    }
}
