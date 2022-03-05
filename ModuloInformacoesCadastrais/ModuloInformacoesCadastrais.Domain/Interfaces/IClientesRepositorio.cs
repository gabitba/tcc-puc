using ModuloInformacoesCadastrais.Domain.Entidades;

namespace ModuloInformacoesCadastrais.Domain.Interfaces
{
    public interface IClientesRepositorio
    {
        Cliente ObterCliente(string id);
    }
}
