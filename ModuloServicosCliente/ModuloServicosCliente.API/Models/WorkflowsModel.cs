namespace ModuloServicosCliente.API.Models
{
    public static class WorkflowsModel
    {
        public const string EnviarReportCliente = "enviarReportCliente";
    }

    public class DeployWorkflowResponse
    {
        public int Version { get; set; }
    }
}
