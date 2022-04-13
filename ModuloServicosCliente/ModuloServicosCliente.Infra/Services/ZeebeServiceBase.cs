using Microsoft.Extensions.Options;
using ModuloServicosCliente.Application.Interfaces;
using ModuloServicosCliente.Infra.Options;
using System.Text.Json;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosCliente.Infra.Services
{
    public class ZeebeServiceBase : IZeebeService
    {
        protected IZeebeClient client;
        protected IOptions<CamundaCloudWorkerOptions> workerOptions;

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

        public async Task<int> DeployWorkflow(string fileBpmn)
        {
            var response = await client.NewDeployCommand().AddResourceFile(fileBpmn).Send();
            return response.Processes[0].Version;
        }
    }
}
