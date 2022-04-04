using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using ModuloInformacoesCadastrais.API.Authorization;
using ModuloInformacoesCadastrais.API.Models;
using ModuloInformacoesCadastrais.Application.DTOs;
using ModuloInformacoesCadastrais.Application.Interfaces;

namespace ModuloInformacoesCadastrais.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Clientes/ComAutorizacao")]
    public class ClientesWithAuthController : BaseController
    {
        private readonly ILogger<ClientesController> logger;
        private readonly IClientesService clientesServico;

        public ClientesWithAuthController(ILogger<ClientesController> logger, IClientesService clientesServico) : base(logger)
        {
            this.logger = logger;
            this.clientesServico = clientesServico;
        }

        /// <summary>
        /// Retorna lista de todos os clientes cadastrados.
        /// </summary>
        /// <returns>Lista de clientes cadastrados.</returns>
        /// <response code="200">Lista de clientes cadastrados.</response>
        /// <response code="500">Erro.</response>
        [HttpGet("")]
        [Authorize(policy: AuthorizationResources.Client.READ_POLICY_NAME)]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<ClienteModel>>> ObterClientesAsync()
        {
            logger.LogInformation($"Nova requisicao {nameof(ObterClientesAsync)}.");

            try
            {
                var clientes = await clientesServico.ObterClientesAsync();

                return Ok(clientes.Select(cliente => ClienteModel.ConvertToModel(cliente)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }

        }

        /// <summary>
        /// Retorna o cliente pelo seu id.
        /// </summary>
        /// <returns>Cliente.</returns>
        /// <response code="200">Retorna o cliente cadastrado.</response>
        /// <response code="404">Cliente n�o encontrado.</response>
        [HttpGet("{idCliente}")]
        [Authorize(policy: AuthorizationResources.Client.READ_POLICY_NAME)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<ClienteModel>> ObterClienteAsync([FromRoute] int idCliente)
        {
            logger.LogInformation($"Nova requisicao {nameof(ObterClienteAsync)}.", idCliente);

            try
            {
                var cliente = await clientesServico.ObterClienteAsync(idCliente);

                if (cliente == null)
                {
                    return NotFound();
                }

                return Ok(ClienteModel.ConvertToModel(cliente));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Cadastra cliente.
        /// </summary>
        /// <returns>Id do cliente cadastrado.</returns>
        /// <response code="200">Retorna o id do cliente cadastrado.</response>
        /// <response code="400">Falta dados para cadastro de cliente.</response>
        [HttpPost]
        [Authorize(policy: AuthorizationResources.Client.READ_OR_WRITE_POLICY_NAME)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<CadastrarClienteResponseModel>> CadastrarClienteAsync([FromBody] CadastrarClienteRequestModel novoCliente)
        {
            logger.LogInformation($"Nova requisicao {nameof(CadastrarClienteAsync)}.", novoCliente);

            try
            {
                if (novoCliente == null)
                {
                    return BadRequest();
                }

                var novoClienteDTO = new ClienteDTO
                {
                    Nome = novoCliente.Nome,
                    Endereco = novoCliente.Endereco
                };

                var idCliente = await clientesServico.CadastrarClienteAsync(novoClienteDTO);

                return Ok(new CadastrarClienteResponseModel
                {
                    id = idCliente,
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Remove cliente cadastrado.
        /// </summary>
        /// <response code="200">Retorna o id do cliente removido.</response>
        /// <response code="400">Falta id de cliente.</response>
        /// <response code="404">Cliente inexistente.</response>
        [HttpDelete("{idCliente}")]
        [Authorize(policy: AuthorizationResources.Client.READ_OR_WRITE_POLICY_NAME)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult> RemoverClienteAsync([FromRoute] int idCliente)
        {
            logger.LogInformation($"Nova requisicao {nameof(RemoverClienteAsync)}.", idCliente);

            try
            {
                if (idCliente == 0)
                {
                    return BadRequest();
                }

                bool removido = await clientesServico.RemoverClienteAync(idCliente);
                if (!removido)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}