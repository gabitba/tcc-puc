namespace ModuloEmail.API
{
    public class EnviarEmailReportClienteModelRequest
    {
        public string Destinatario { get; set; }

        public ClienteModel Cliente { get; set; }
    }

    public class ClienteModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Endereco { get; set; }
    }
}