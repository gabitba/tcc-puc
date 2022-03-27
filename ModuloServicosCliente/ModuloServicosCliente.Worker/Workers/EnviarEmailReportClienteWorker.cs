using ModuloServicosCliente.Application.DTOs;
using ModuloServicosCliente.Application.Interfaces;
using System.Text.Json;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosCliente.Workers
{
    public class EnviarEmailReportClienteWorker : BaseWorker
    {
        private readonly ILogger<EnviarEmailReportClienteWorker> logger;
        private readonly IEmailService emailService;

        public EnviarEmailReportClienteWorker(
            ILogger<EnviarEmailReportClienteWorker> logger,
            IZeebeService camundaService,
            IEmailService emailService) : base(logger, camundaService)
        {
            this.logger = logger;
            this.emailService = emailService;
        }

        protected override string JobType => "enviarEmailReportCliente";

        protected override async Task JobHandler(IJobClient jobClient, IJob activatedJob)
        {
            try
            {
                logger.LogInformation($"{activatedJob}: Processando job.");

                var variables = JsonSerializer.Deserialize<Dictionary<string, string>>(activatedJob.Variables);
                int clienteId = Convert.ToInt32(variables["clienteId"]);
                string clienteNome = variables["clienteNome"];
                string clienteEndereco = variables["clienteEndereco"];
                string destinatario = variables["destinatario"];

                logger.LogInformation($"{activatedJob}: Enviando e-mail de report do cliente {clienteId}");

                await emailService.EnviarEmailReportCliente(destinatario, new ClienteDTO
                {
                    Id = clienteId,
                    Nome = clienteNome,
                    Endereco = clienteEndereco,
                });

                logger.LogInformation($"{activatedJob}: E-mail enviado.");
                await jobClient.NewCompleteJobCommand(activatedJob.Key).Send();
            } 
            catch (Exception ex)
            {
                logger.LogError($"{activatedJob}: Erro durante processamento. {ex.Message}.", ex);
                await jobClient.NewThrowErrorCommand(activatedJob.Key).ErrorCode("500").Send();
            }
        }
    }
}