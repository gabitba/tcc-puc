using ModuloInformacoesCadastrais.Application.DTOs;
using ModuloInformacoesCadastrais.Application.Interfaces;
using ModuloInformacoesCadastrais.Domain.Entidades;
using ModuloInformacoesCadastrais.Domain.Interfaces;

namespace ModuloInformacoesCadastrais.Application.Servicos
{
    public class ClientesServicos : IClientesServico
    {
        readonly IClientesRepositorio clientesRepository;

        public ClientesServicos(IClientesRepositorio clientesRepository)
        {
            this.clientesRepository = clientesRepository;
        }

        public ClienteDTO ObterCliente(string id)
        {
            Cliente cliente = clientesRepository.ObterCliente(id);
            return new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome
            };
        }
    }
}
