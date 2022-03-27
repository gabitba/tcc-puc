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
                int clienteId = Convert.ToInt32(variables["clienteId"]);

                logger.LogInformation($"{activatedJob}: Buscando dados do cliente {clienteId}");
                ClienteDTO cliente = await clienteService.ObterClienteAsync(clienteId);
                string clienteOutput = JsonSerializer.Serialize(new Output
                {
                    Destinatario = variables["destinatario"],
                    ClienteId = cliente.Id,
                    ClienteNome = cliente.Nome,
                    ClienteEndereco = cliente.Endereco,
                });

                logger.LogInformation($"{activatedJob}: Obtido dados do cliente: {clienteOutput}");
                await jobClient.NewCompleteJobCommand(activatedJob.Key)
                    .Variables(clienteOutput).Send();
            } 
            catch (Exception ex)
            {
                logger.LogError($"{activatedJob}: Erro durante processamento. {ex.Message}.", ex);
                await jobClient.NewThrowErrorCommand(activatedJob.Key).ErrorCode("500").Send();
            }
        }

        internal class Output
        {
            public string Destinatario { get; set; }

            public int ClienteId { get; set; }

            public string ClienteNome { get; set; } = string.Empty;

            public string ClienteEndereco { get; set; } = string.Empty;
        }
    }
}