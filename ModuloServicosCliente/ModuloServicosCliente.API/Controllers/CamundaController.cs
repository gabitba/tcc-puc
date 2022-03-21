using Microsoft.AspNetCore.Mvc;
using ModuloServicosCliente.API.Models;
using ModuloServicosCliente.Application.Interfaces;

namespace ModuloServicosCliente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CamundaController : ControllerBase
    {
        private readonly ILogger<CamundaController> logger;
        private readonly IZeebeService camundaService;

        public CamundaController(ILogger<CamundaController> logger, IZeebeService camundaService)
        {
            this.logger = logger;
            this.camundaService = camundaService;
        }

        /// <summary>
        /// Começa a instancia do processo de obter report de cliente.
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
        public async Task<ActionResult<StartProcessoResponseModel>> StartProcessoObterReportCliente([FromBody] StartProcessoCriarReportClienteRequest novoProcesso)
        {
            if (novoProcesso == null || string.IsNullOrWhiteSpace(novoProcesso.ClienteId))
            {
                return BadRequest();
            }

            try
            {
                var instancia = await camundaService.ComecarInstanciaProcesso(ProcessosBPMN.ObterReportCliente, new Dictionary<string, string>());

                return Ok(new StartProcessoResponseModel
                {
                    IdProcesso = instancia.BpmnProcessId,
                    KeyDefinicaoProcesso = instancia.ProcessDefinitionKey,
                    Versao = instancia.Version,
                    KeyInstancia = instancia.ProcessInstanceKey,
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode(500, "Erro inesperado.");
            }
        }

        /// <summary>
        /// Começa a instancia de um processo BPMN genérico por seu id.
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
        public async Task<ActionResult<StartProcessoResponseModel>> ComecarInstanciaProcesso([FromBody] StartProcessoRequestModel novoProcesso)
        {
            if (novoProcesso == null || string.IsNullOrWhiteSpace(novoProcesso.IdProcesso))
            {
                return BadRequest();
            }

            try
            {
                var instancia = await camundaService.ComecarInstanciaProcesso(novoProcesso.IdProcesso, novoProcesso.Variaveis);

                return Ok(new StartProcessoResponseModel
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