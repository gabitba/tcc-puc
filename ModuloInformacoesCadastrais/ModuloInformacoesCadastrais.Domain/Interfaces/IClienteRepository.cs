using ModuloInformacoesCadastrais.Domain.Entities;

namespace ModuloInformacoesCadastrais.Domain.Interfaces
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> ObterClientes();

        Cliente ObterCliente(string id);
    }
}
