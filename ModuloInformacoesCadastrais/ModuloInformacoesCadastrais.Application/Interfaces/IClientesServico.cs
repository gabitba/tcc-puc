using ModuloInformacoesCadastrais.Application.DTOs;

namespace ModuloInformacoesCadastrais.Application.Interfaces
{
    public interface IClientesServico
    {
        ClienteDTO ObterCliente(string id);
    }
}
