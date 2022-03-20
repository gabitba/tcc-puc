using Microsoft.Extensions.Options;
using ModuloServicosClienteWorker.Infra.Options;
using System.Text.Json;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;
using Zeebe.Client.Impl.Builder;

namespace ModuloServicosClienteWorker.Infra.Services
{
    public class CamundaService : ICamundaService
    {
        readonly IZeebeClient client;
        readonly IOptions<CamundaCloudWorkerOptions> workerOptions;

        public CamundaService(IOptions<CamundaCloudClientOptions> clientOptions, IOptions<CamundaCloudWorkerOptions> workerOptions)
        {
            this.workerOptions = workerOptions;

            client = CamundaCloudClientBuilder
                .Builder()
                .UseClientId(clientOptions.Value.ClientId)
                .UseClientSecret(clientOptions.Value.ClientSecret)
                .UseContactPoint(clientOptions.Value.ContactPoint)
                .UseAuthServer(clientOptions.Value.AuthServer)
                //.UseLoggerFactory(LoggerFactory) // optional
                .Build();
        }

        public async Task<IProcessInstanceResponse> ComecarInstanciaProcesso(string bpmnProcessId, IDictionary<object, object> variablesJson)
        {
            string variables = JsonSerializer.Serialize(variablesJson);
            return await client
                .NewCreateProcessInstanceCommand()
                .BpmnProcessId(bpmnProcessId)
                .LatestVersion()
                .Variables(variables).Send();
        }

        public IJobWorker CriarWorker(string jobType, AsyncJobHandler jobHandler, string workerName)
        {
            return client.NewWorker()
                .JobType(jobType)
                .Handler(jobHandler)
                .MaxJobsActive(workerOptions.Value.MaxJobActive)
                .Timeout(TimeSpan.FromSeconds(workerOptions.Value.Timeout))
                .PollInterval(TimeSpan.FromSeconds(workerOptions.Value.PollInterval))
                .PollingTimeout(TimeSpan.FromSeconds(workerOptions.Value.PollingTimeout))
                .Name(workerName)
                .Open();
        }
    }
}
