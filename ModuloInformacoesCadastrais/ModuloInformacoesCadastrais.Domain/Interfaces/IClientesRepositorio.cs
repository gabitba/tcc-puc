using ModuloInformacoesCadastrais.Domain.Entidades;

namespace ModuloInformacoesCadastrais.Domain.Interfaces
{
    public interface IClientesRepositorio
    {
        Task<IEnumerable<Cliente>> ObterClientesAsync();

        Task<Cliente> ObterClienteAsync(int idCliente);

        Task<int> CadastrarClienteAsync(Cliente cliente);

        Task<bool> RemoverClienteAync(int idCliente);
    }
}
