using System.ComponentModel.DataAnnotations;

namespace ModuloServicosCliente.API.Models
{
    public class StartProcessoCriarReportClienteRequest
    {
        [Required]
        public string ClienteId { get; set; } = string.Empty;
    }
}
