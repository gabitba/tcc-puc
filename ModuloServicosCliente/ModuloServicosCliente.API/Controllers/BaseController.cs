using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ModuloServicosCliente.API.Controllers
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
            logger.LogError(RemoveSpecialCharacters(ex.Message), ex.StackTrace);
            return StatusCode(500, "Erro inesperado.");
        }

        static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_. ;]+", "", RegexOptions.Compiled);
        }
    }
}
