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
            logger.LogError(ex.Message, ex.StackTrace);
            return StatusCode(500, "Erro inesperado.");
        }
    }
}
