using Microsoft.AspNetCore.Mvc;
using ModuloServicosCliente.API.Models;
using ModuloServicosCliente.Application.Interfaces;

namespace ModuloServicosCliente.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessosController : BaseController
    {
        private readonly ILogger<ProcessosController> logger;
        private readonly IZeebeService camundaService;

        public ProcessosController(ILogger<ProcessosController> logger, IZeebeService camundaService) : base(logger)
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
        public async Task<ActionResult<StartProcessoResponseModel>> StartProcessoObterReportClienteAsync([FromBody] StartProcessoCriarReportClienteRequest novoProcesso)
        {
            logger.LogInformation($"Nova requisicao {nameof(StartProcessoObterReportClienteAsync)}.", novoProcesso);

            if (novoProcesso == null || string.IsNullOrWhiteSpace(novoProcesso.ClienteId))
            {
                return BadRequest();
            }
            try
            {
                var instancia = await camundaService.ComecarInstanciaProcessoAsync(ProcessosBPMN.ObterReportCliente, new Dictionary<string, string>());

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
                return HandleError(ex);
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
        public async Task<ActionResult<StartProcessoResponseModel>> ComecarInstanciaProcessoAsync([FromBody] StartProcessoRequestModel novoProcesso)
        {
            logger.LogInformation($"Nova requisicao {nameof(ComecarInstanciaProcessoAsync)}.", novoProcesso);

            if (novoProcesso == null || string.IsNullOrWhiteSpace(novoProcesso.IdProcesso))
            {
                return BadRequest();
            }
            try
            {
                var instancia = await camundaService.ComecarInstanciaProcessoAsync(novoProcesso.IdProcesso, novoProcesso.Variaveis);

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
                return HandleError(ex);
            }
        }
    }
}