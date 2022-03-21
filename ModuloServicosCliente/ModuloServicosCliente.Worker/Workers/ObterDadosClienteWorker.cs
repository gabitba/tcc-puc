using ModuloServicosCliente.Application.DTOs;
using ModuloServicosCliente.Application.Interfaces;
using System.Text.Json;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosCliente.Workers
{
    public class ObterDadosClienteWorker : BaseWorker
    {
        private readonly ILogger<ObterDadosClienteWorker> logger;
        private readonly IClienteService clienteService;

        public ObterDadosClienteWorker(
            ILogger<ObterDadosClienteWorker> logger,
            IZeebeService camundaService,
            IClienteService clienteService) : base(logger, camundaService)
        {
            this.logger = logger;
            this.clienteService = clienteService;
        }

        protected override string JobType => "obterDadosCliente";

        protected override async Task JobHandler(IJobClient jobClient, IJob activatedJob)
        {
            try
            {
                logger.LogInformation($"{activatedJob}: Processando job.");

                var variables = JsonSerializer.Deserialize<Dictionary<string, string>>(activatedJob.Variables);
                int clienteId = Convert.ToInt32(variables["clienteid"]);
                logger.LogInformation($"{activatedJob}: Buscando dados do cliente {clienteId}");

                ClienteDTO cliente = await clienteService.ObterClienteAsync(clienteId);

                await jobClient.NewCompleteJobCommand(activatedJob.Key)
                    .Variables(JsonSerializer.Serialize(cliente)).Send();
            } catch (Exception ex)
            {
                logger.LogError($"{activatedJob}: Erro durante processamento. {ex.Message}.", ex);
                await jobClient.NewThrowErrorCommand(activatedJob.Key).ErrorCode("500").Send();
            }
        }
    }
}