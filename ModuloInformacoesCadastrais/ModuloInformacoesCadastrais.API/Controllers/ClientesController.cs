using Microsoft.AspNetCore.Mvc;
using ModuloInformacoesCadastrais.API.Models;
using ModuloInformacoesCadastrais.Application.DTOs;
using ModuloInformacoesCadastrais.Application.Interfaces;

namespace ModuloInformacoesCadastrais.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<ClientesController> logger;
        private readonly IClientesServico clientesServico;

        public ClientesController(ILogger<ClientesController> logger, IClientesServico clientesServico)
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
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<ClienteModel>>> ObterClientesAsync()
        {
            logger.Log(LogLevel.Information, "Chegou requisição.");

            var clientes = await clientesServico.ObterClientesAsync();

            return Ok(clientes.Select(cliente => new ClienteModel
            { 
                Id = cliente.Id,
                Nome = cliente.Nome,
                Endereco = cliente.Endereco,
            }));
        }

        /// <summary>
        /// Retorna o cliente pelo seu id.
        /// </summary>
        /// <returns>Cliente.</returns>
        /// <response code="200">Retorna o cliente cadastrado.</response>
        /// <response code="404">Cliente não encontrado.</response>
        [HttpGet("{idCliente}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<ClienteModel>> ObterClienteAsync([FromRoute] int idCliente)
        {
            logger.Log(LogLevel.Information, $"Chegou requisição: {idCliente}");

            var cliente = await clientesServico.ObterClienteAsync(idCliente);

            if(cliente == null)
            {
                return NotFound();
            }

            return Ok(new ClienteModel { 
                Id = cliente.Id,
                Nome = cliente.Nome,
            });
        }

        /// <summary>
        /// Cadastra cliente.
        /// </summary>
        /// <returns>Id do cliente cadastrado.</returns>
        /// <response code="200">Retorna o id do cliente cadastrado.</response>
        /// <response code="400">Falta dados para cadastro de cliente.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Produces("application/json")]
        public async Task<ActionResult<CadastrarClienteResponseModel>> CadastrarClienteAsync([FromBody] CadastrarClienteRequestModel novoCliente)
        {
            if(novoCliente == null)
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

        /// <summary>
        /// Remove cliente cadastrado.
        /// </summary>
        /// <response code="200">Retorna o id do cliente removido.</response>
        /// <response code="400">Falta id de cliente.</response>
        /// <response code="404">Cliente inexistente.</response>
        [HttpDelete("{idCliente}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult> RemoverClienteAsync([FromRoute]int idCliente)
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
}

}