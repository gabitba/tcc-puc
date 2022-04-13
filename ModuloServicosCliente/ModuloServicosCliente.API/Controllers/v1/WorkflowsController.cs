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
        /// Realiza o deploy do workflow do processo de envio de report de cliente por e-mail. O arquivo do .bpmn deployado se encontra no caminho "./Workflows/enviarReportCliente.bpmn".
        /// </summary>
        /// <response code="200">Workflow BPMN deployado.</response>
        /// <response code="500">Houve erro no deploy do workflow.</response>
        [HttpPost("EmailReportCliente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<ActionResult> DeployWorkflowEnviarReportClienteAsync()
        {
            logger.LogInformation($"Nova requisicao {nameof(DeployWorkflowEnviarReportClienteAsync)}.");

            try
            {
                await camundaService.DeployWorkflow($"./Workflows/{ProcessosBpmn.EnviarReportCliente}.bpmn");
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}