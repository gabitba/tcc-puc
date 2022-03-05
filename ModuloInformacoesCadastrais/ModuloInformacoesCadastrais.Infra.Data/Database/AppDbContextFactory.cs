using ModuloInformacoesCadastrais.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloInformacoesCadastrais.Infra.Data.Database
{
    internal class AppDbContextFactory : IDbContextFactory<AppDbContext>
    {
        //TODO put connection string
        public AppDbContext Create()
        {
            return new AppDbContext("Data Source=host.docker.internal,1433;Initial Catalog=Master;User ID=sa;Password=Gabs!Tcc");
        }
    }
}
