using System.ComponentModel.DataAnnotations;

namespace ModuloServicosCliente.API.Models
{
    public class EnviarReportClienteModelRequest
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public string Destinatario { get; set; }
    }
}
