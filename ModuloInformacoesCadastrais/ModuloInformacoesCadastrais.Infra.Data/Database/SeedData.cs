using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModuloInformacoesCadastrais.Domain.Entidades;
using ModuloInformacoesCadastrais.Infra.Data.Context;

namespace ModuloInformacoesCadastrais.Infra.Data.Database
{
    public static class SeedData
    {
        public static void PreencherDados(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (context.Clientes.Any())
                {
                    return;
                }

                context.Clientes.AddRange(
                    new Cliente
                    {
                        Id = 1,
                        Nome = "Granada",
                        Endereco = "Rua Kennedy, 30, Bairro York",
                    },
                    new Cliente
                    {
                        Id = 2,
                        Nome = "Run Ball",
                        Endereco = "Avenida Portal do Mar, 11, Bairro Norte",
                    },
                    new Cliente
                    {
                        Id = 3,
                        Nome = "Shoesmaster",
                        Endereco = "Rua Ratagatinga, 1346, Bairro Betunia",
                    },
                    new Cliente
                    {
                        Id = 4,
                        Nome = "Mesa do Papa",
                        Endereco = "Rua Almeida, 780, Bairro Heliópolis",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
