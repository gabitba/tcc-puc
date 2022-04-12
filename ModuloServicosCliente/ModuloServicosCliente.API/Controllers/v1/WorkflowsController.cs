using Microsoft.AspNetCore.Mvc;
using ModuloServicosCliente.API.Models;
using ModuloServicosCliente.Application.Interfaces;

namespace ModuloServicosCliente.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WorkflowsController : BaseController
    {
        private readonly ILogger<WorkflowsController> logger;
        private readonly IZeebeService camundaService;

        public WorkflowsController(ILogger<WorkflowsController> logger, IZeebeService camundaService) : base(logger)
        {
            this.logger = logger;
            this.camundaService = camundaService;
        }


        /// <summary>
        /// Atualiza o workflow BPMN do processo de envio de report de cliente por e-mail.
        /// </summary>
        /// <remarks>
        /// O workflow é atualizado com o arquivo ./BPMN/enviarReportCliente.bpmn.
        /// </remarks>
        /// <response code="200">Workflow BPMN atualizado.</response>
        /// <response code="500">Houve erro na atualização do workflow.</response>
        [HttpPost("EmailReportCliente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<ActionResult> AtualizarWorkflowProcessoEnviarReportClienteAsync()
        {
            logger.LogInformation($"Nova requisicao {nameof(AtualizarWorkflowProcessoEnviarReportClienteAsync)}.");

            try
            {
                await camundaService.AtualizarWorkflowProcesso($"Bpmn/{ProcessosBpmn.EnviarReportCliente}.bpmn");
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}