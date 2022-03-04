using Microsoft.AspNetCore.Mvc;
using ModuloInformacoesCadastrais.API.Models;
using ModuloInformacoesCadastrais.Domain.Interfaces;

namespace ModuloInformacoesCadastrais.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(ILogger<ClientesController> logger, IClienteRepository clienteRepository)
        {
            _logger = logger;
            _clienteRepository = clienteRepository;
        }

        /// <summary>
        /// Retorna lista de todos os clientes cadastrados.
        /// </summary>
        /// <returns>Lista de objetos ClienteDTO</returns>
        /// <response code="200">Retorna os clientes cadastrados</response>
        [HttpGet("")]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public IEnumerable<ClienteDTO> ObterClientes()
        {
            _logger.Log(LogLevel.Information, "Chegou requisição.");

            var clientes = _clienteRepository.ObterClientes();

            return clientes.Select(cliente => new ClienteDTO { Id = cliente.Id });
        }

        /// <summary>
        /// Retorna o cliente pelo seu id.
        /// </summary>
        /// <returns>Um objeto ClienteDTO</returns>
        /// <response code="200">Retorna os clientes cadastrados</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public ClienteDTO ObterCliente(string id)
        {
            _logger.Log(LogLevel.Information, $"Chegou requisição: {id}");

            var cliente = _clienteRepository.ObterCliente(id);

            return new ClienteDTO { Id = cliente.Id };
        }
    }
}