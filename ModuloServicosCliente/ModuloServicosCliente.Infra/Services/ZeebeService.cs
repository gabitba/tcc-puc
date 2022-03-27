using Microsoft.Extensions.Options;
using ModuloServicosCliente.Application.Interfaces;
using ModuloServicosCliente.Infra.Options;
using System.Text.Json;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;
using Zeebe.Client.Impl.Builder;

namespace ModuloServicosCliente.Infra.Interfaces
{
    public class ZeebeService : IZeebeService
    {
        readonly IZeebeClient client;
        readonly IOptions<CamundaCloudWorkerOptions> workerOptions;

        public ZeebeService(IOptions<CamundaCloudClientOptions> clientOptions, IOptions<CamundaCloudWorkerOptions> workerOptions)
        {
            this.workerOptions = workerOptions;

            // Usar esse client builder quando for usar o camunda cloud SaaS
            //client = CamundaCloudClientBuilder
            //.Builder()
            //.UseClientId(clientOptions.Value.ClientId)
            //.UseClientSecret(clientOptions.Value.ClientSecret)
            //.UseContactPoint(clientOptions.Value.ContactPoint)
            //.UseAuthServer(clientOptions.Value.AuthServer)
            //.UseLoggerFactory(LoggerFactory) // optional
            //.Build();

            client = ZeebeClient
                .Builder()
                .UseGatewayAddress(clientOptions.Value.ContactPoint)
                .UsePlainText()
                .Build();
        }

        public async Task<IProcessInstanceResponse> StartInstanciaProcessoAsync(string bpmnProcessId, IDictionary<string, string> variablesJson)
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

        public async Task DeployInstanciaProcessoAsync(string idProcesso)
        {
            await client.NewDeployCommand().AddResourceFile($"BPMN/{idProcesso}.bpmn").Send();
        }

    }
}
