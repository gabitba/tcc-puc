using Microsoft.AspNetCore.Mvc;
using ModuloServicosCliente.API.Models;
using ModuloServicosClienteWorker.Infra.Services;

namespace ModuloServicosCliente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CamundaController : ControllerBase
    {
        private readonly ILogger<CamundaController> logger;
        private readonly ICamundaService camundaService;

        public CamundaController(ILogger<CamundaController> logger, ICamundaService camundaService)
        {
            this.logger = logger;
            this.camundaService = camundaService;
        }

        /// <summary>
        /// Começa a instancia de um processo BPMN por seu id.
        /// </summary>
        /// <returns>Informações da instancia de Processo BPMN.</returns>
        /// <response code="200">Instancia criada.</response>
        /// <response code="400">Falta dados para criação da instancia.</response>
        /// <response code="500">Houve erro na criação da instancia.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<ActionResult<ComecarInstanciaProcessoResponseModel>> ComecarInstanciaProcesso([FromBody] CriarInstanciaProcessoRequestModel novoProcesso)
        {
            if (novoProcesso == null || string.IsNullOrWhiteSpace(novoProcesso.IdProcesso))
            {
                return BadRequest();
            }

            try
            {
                var instancia = await camundaService.ComecarInstanciaProcesso(novoProcesso.IdProcesso, novoProcesso.Variaveis);

                return Ok(new ComecarInstanciaProcessoResponseModel
                {
                    IdProcesso = instancia.BpmnProcessId,
                    KeyDefinicaoProcesso = instancia.ProcessDefinitionKey,
                    Versao = instancia.Version,
                    KeyInstancia = instancia.ProcessInstanceKey,
                });
            } catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode(500, "Erro inesperado.");
            }
        }
    }
}