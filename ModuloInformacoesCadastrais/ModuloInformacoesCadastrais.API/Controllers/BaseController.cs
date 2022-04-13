using Microsoft.AspNetCore.Mvc;

namespace ModuloInformacoesCadastrais.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> logger;

        protected BaseController(ILogger<BaseController> logger)
        {
            this.logger = logger;
        }

        protected ObjectResult HandleError(Exception ex)
        {
            logger.LogError(ex, ex.Message);

            //Nunca deve expor detalhes do erro para o cliente. Só foi feito aqui por questões de ser uma aplicação interna para prototipação. 
            return StatusCode(500, $"Erro inesperado. Error: {ex.Message}");
        }
    }
}
