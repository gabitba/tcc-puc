using ModuloServicosCliente.Application.DTOs;

namespace ModuloServicosCliente.Application.Interfaces
{
    public interface IEmailService
    {
        Task EnviarEmailReportCliente(ClienteDTO cliente);
    }
}
