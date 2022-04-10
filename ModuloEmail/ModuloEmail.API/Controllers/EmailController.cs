using Microsoft.AspNetCore.Mvc;
using System.Text;
using static ModuloEmail.API.EmailService;

namespace ModuloEmail.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<Controller> logger;
        private readonly EmailService emailService;

        public EmailController(ILogger<Controller> logger, EmailService emailService)
        {
            this.logger = logger;
            this.emailService = emailService;
        }

        [HttpPost("ReportCliente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<ActionResult> EnviarEmailReportCliente([FromBody] EnviarEmailReportClienteModelRequest request)
        {
            if(request == null)
            {
                return BadRequest("Favor preencher a request.");
            }

            if (request.Cliente == null)
            {
                return BadRequest("Favor preencher os dados de cliente.");
            }

            try
            {
                StringBuilder emailBuilder = new StringBuilder("<HTML><Body><p>Caros,</p>");
                emailBuilder.Append("<p>Venho por meio desta apresentar um texto bem grande para experimentar o envio de e-mails usando tags HTML.<BR/>");
                emailBuilder.Append("O conteúdo aqui é <b>irrelevante</b>, tendo como objetivo <i>validar</i> o recebimento de dados de cliente:</p>");
                emailBuilder.Append($"<ul>Cliente Id: {request.Cliente.Id}</ul>");
                emailBuilder.Append($"<ul>Nome do Cliente: {request.Cliente.Nome}</ul>");
                emailBuilder.Append($"<ul>Endereço do Cliente: {request.Cliente.Endereco}</ul>");
                emailBuilder.Append("<p>Quaisquer dúvidas, estou à disposição.</p>");
                emailBuilder.Append("<p>Atenciosamente,</p>");
                emailBuilder.Append("<p>Envio de E-mail <BR/> Boa Entrega </p></Body></HTML>");

                EmailConteudo conteudo = new EmailConteudo
                {
                    IsHtml = true,
                    To = request.Destinatario,
                    Subject = "Teste Envio Email",
                    Body = emailBuilder.ToString(),
                };

                await emailService.EnviarEmailAsync(conteudo);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.StackTrace);
                return StatusCode(500, "Erro inesperado.");
            }
        }
    }
}