using ModuloInformacoesCadastrais.Domain.Entities;
using ModuloInformacoesCadastrais.Domain.Interfaces;

namespace ModuloInformacoesCadastrais.Domain.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        public Cliente ObterCliente(string id)
        {
            return new Cliente
            {
                Id = id,
            };
        }

        public IEnumerable<Cliente> ObterClientes()
        {
            return Enumerable.Empty<Cliente>();
        }
    }
}
