using ModuloInformacoesCadastrais.Domain.Entidades;
using ModuloInformacoesCadastrais.Domain.Interfaces;
using ModuloInformacoesCadastrais.Infra.Data.Context;

namespace ModuloInformacoesCadastrais.Domain.Repositorios
{
    public class ClientesRepositorio : IClientesRepositorio
    {
        readonly AppDbContext dbContext;

        public ClientesRepositorio(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Cliente>> ObterClientesAsync()
        {
            return dbContext.Clientes;
        }

        public async Task<Cliente> ObterClienteAsync(int idCliente)
        {
            return await dbContext.Clientes.FindAsync(idCliente);
        }

        public async Task<int> CadastrarClienteAsync(Cliente cliente)
        {
            var created = await dbContext.Clientes.AddAsync(cliente);
            await dbContext.SaveChangesAsync();
            return created.Entity.Id;
        }

        public async Task<bool> RemoverClienteAync(int idCliente)
        {
            var cliente = await dbContext.Clientes.FindAsync(idCliente);

            if(cliente == null)
            {
                return false;
            }

            dbContext.Clientes.Remove(cliente);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
