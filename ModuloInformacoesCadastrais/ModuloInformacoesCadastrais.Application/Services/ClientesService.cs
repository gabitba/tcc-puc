using ModuloInformacoesCadastrais.Application.DTOs;
using ModuloInformacoesCadastrais.Application.Interfaces;
using ModuloInformacoesCadastrais.Domain.Entidades;
using ModuloInformacoesCadastrais.Domain.Interfaces;

namespace ModuloInformacoesCadastrais.Application.Services
{
    public class ClientesService : IClientesService
    {
        readonly IClientesRepositorio clientesRepositorio;

        public ClientesService(IClientesRepositorio clientesRepository)
        {
            this.clientesRepositorio = clientesRepository;
        }


        public async Task<IEnumerable<ClienteDTO>> ObterClientesAsync()
        {
            var clientes = await clientesRepositorio.ObterClientesAsync();

            return clientes.Select(cliente => new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Endereco = cliente.Endereco
            });
        }

        public async Task<ClienteDTO> ObterClienteAsync(int idCliente)
        {
            var cliente = await clientesRepositorio.ObterClienteAsync(idCliente);

            if(cliente == null)
            {
                return null;
            }

            return new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Endereco = cliente.Endereco
            };
        }

        public async Task<int> CadastrarClienteAsync(ClienteDTO cliente)
        {
            var novoCliente = new Cliente
            {
                Nome = cliente.Nome,
                Endereco = cliente.Endereco,
            };

            return await clientesRepositorio.CadastrarClienteAsync(novoCliente);
        }

        public async Task<bool> RemoverClienteAync(int idCliente)
        {
            return await clientesRepositorio.RemoverClienteAync(idCliente);
        }
    }
}
