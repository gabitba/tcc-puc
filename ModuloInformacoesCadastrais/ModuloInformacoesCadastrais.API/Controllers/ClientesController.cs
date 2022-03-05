using Microsoft.AspNetCore.Mvc;
using ModuloInformacoesCadastrais.API.Models;
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

        ///// <summary>
        ///// Retorna lista de todos os clientes cadastrados.
        ///// </summary>
        ///// <returns>Lista de objetos ClienteDTO</returns>
        ///// <response code="200">Retorna os clientes cadastrados</response>
        //[HttpGet("")]
        //[ProducesResponseType(200)]
        //[Produces("application/json")]
        //public IEnumerable<ClienteModel> ObterClientes()
        //{
        //    logger.Log(LogLevel.Information, "Chegou requisição.");

        //    var clientes = clienteRepository.ObterClientes();

        //    return clientes.Select(cliente => new ClienteDTO { Id = cliente.Id });
        //}

        /// <summary>
        /// Retorna o cliente pelo seu id.
        /// </summary>
        /// <returns>Um objeto ClienteDTO</returns>
        /// <response code="200">Retorna os clientes cadastrados</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public ClienteModelo ObterCliente(string id)
        {
            logger.Log(LogLevel.Information, $"Chegou requisição: {id}");

            var cliente = clientesServico.ObterCliente(id);

            return new ClienteModelo { 
                Id = cliente.Id,
                Nome = cliente.Nome,
            };
        }
    }
}