using Microsoft.AspNetCore.Mvc;
using ModuloServicosCliente.API.Models;
using ModuloServicosCliente.Application.Interfaces;

namespace ModuloServicosCliente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessoController : BaseController
    {
        private readonly ILogger<ProcessoController> logger;
        private readonly IZeebeService camundaService;

        public ProcessoController(ILogger<ProcessoController> logger, IZeebeService camundaService) : base(logger)
        {
            this.logger = logger;
            this.camundaService = camundaService;
        }


        /// <summary>
        /// Faz o deploy do bpmn da instancia de processo de enviar report de cliente.
        /// </summary>
        /// <response code="200">Deploy realizado.</response>
        /// <response code="500">Houve erro no deploy.</response>
        [HttpPost("Report/Cliente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<ActionResult> DeployProcessoEnviarReportClienteAsync()
        {
            logger.LogInformation($"Nova requisicao {nameof(DeployProcessoEnviarReportClienteAsync)}.");

            try
            {
                await camundaService.DeployInstanciaProcessoAsync(ProcessosBPMN.EnviarReportCliente);
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}