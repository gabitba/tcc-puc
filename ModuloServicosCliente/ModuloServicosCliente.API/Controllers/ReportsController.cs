﻿using Microsoft.AspNetCore.Mvc;
using ModuloServicosCliente.API.Models;
using ModuloServicosCliente.Application.Interfaces;

namespace ModuloServicosCliente.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : BaseController
    {
        private readonly ILogger<ReportsController> logger;
        private readonly IZeebeService camundaService;

        public ReportsController(ILogger<ReportsController> logger, IZeebeService camundaService) : base(logger)
        {
            this.logger = logger;
            this.camundaService = camundaService;
        }

        /// <summary>
        /// Enviar report de cliente por e-mail.
        /// </summary>
        /// <returns>Informações da instancia de Processo BPMN.</returns>
        /// <response code="200">Enviada a instancia de processo.</response>
        /// <response code="400">Falta dados para envio da instancia.</response>
        /// <response code="500">Houve erro no envio da instancia.</response>
        [HttpPost("Cliente/{clienteId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<ActionResult<StartInstanciaProcessoResponseModel>> EnviarReportClienteAsync([FromRoute] int clienteId, [FromBody] string destinatario)
        {
            logger.LogInformation($"Nova requisicao {nameof(EnviarReportClienteAsync)}.", clienteId);

            if (string.IsNullOrWhiteSpace(destinatario))
            {
                return BadRequest("Precisa preencher o destinatario do e-mail.");
            }

            try
            {
                var variaveis = new Dictionary<string, string>();
                variaveis.Add("clienteId", clienteId.ToString());
                variaveis.Add("destinatario", destinatario);

                var instancia = await camundaService.StartInstanciaProcessoAsync(ProcessosBpmn.EnviarReportCliente, variaveis);

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