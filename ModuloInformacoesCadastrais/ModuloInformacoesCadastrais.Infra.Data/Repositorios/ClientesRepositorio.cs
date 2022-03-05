using ModuloInformacoesCadastrais.Domain.Entidades;
using ModuloInformacoesCadastrais.Domain.Interfaces;

namespace ModuloInformacoesCadastrais.Domain.Repositorios
{
    public class ClientesRepositorio : IClientesRepositorio
    {
        public Cliente ObterCliente(string id)
        {
            return new Cliente
            {
                Id = id,
            };
        }
    }
}
