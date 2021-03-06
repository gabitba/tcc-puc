using Microsoft.AspNetCore.Mvc;
using ModuloServicosCliente.API.Models;
using ModuloServicosCliente.Application.Interfaces;
using System.Text.RegularExpressions;

namespace ModuloServicosCliente.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmailsController : BaseController
    {
        private readonly ILogger<EmailsController> logger;
        private readonly IZeebeService camundaService;

        public EmailsController(ILogger<EmailsController> logger, IZeebeService camundaService) : base(logger)
        {
            this.logger = logger;
            this.camundaService = camundaService;
        }

        /// <summary>
        /// Cria uma nova instância de processo de envio de report de cliente por e-mail.
        /// </summary>
        /// <returns>Informações da instancia de processo criada.</returns>
        /// <response code="200">Criada a instancia de processo.</response>
        /// <response code="400">Falta dados para criação da instancia de proccesso.</response>
        /// <response code="500">Houve erro durante criação da instancia de processo.</response>
        [HttpPost("ReportCliente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<ActionResult<StartInstanciaProcessoResponseModel>> EnviarReportClienteAsync([FromBody] EnviarReportClienteModelRequest request)
        {
            logger.LogInformation($"Nova requisicao {nameof(EnviarReportClienteAsync)}.", request);

            if(request.ClienteId <= 0)
            {
                return BadRequest("Id do cliente inválido");
            }

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(request.Destinatario);
            if (!match.Success)
            {
                return BadRequest("E-mail do destinatário inválido");
            }

            try
            {
                var variaveis = new Dictionary<string, string>();
                variaveis.Add("clienteId", request.ClienteId.ToString());
                variaveis.Add("destinatario", request.Destinatario);

                var instancia = await camundaService.StartInstanciaProcessoAsync(WorkflowsModel.EnviarReportCliente, variaveis);

                return Ok(new StartInstanciaProcessoResponseModel
                {
                    IdProcesso = instancia.BpmnProcessId,
                    KeyDefinicaoProcesso = instancia.ProcessDefinitionKey,
                    Versao = instancia.Version,
                    KeyInstancia = instancia.ProcessInstanceKey,
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}