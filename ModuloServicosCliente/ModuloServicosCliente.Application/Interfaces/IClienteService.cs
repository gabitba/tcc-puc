using ModuloServicosCliente.Application.DTOs;

namespace ModuloServicosCliente.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDTO> ObterClienteAsync(int id);
    }
}
