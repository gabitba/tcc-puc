namespace ModuloServicosClienteWorker.Infra.Options
{
    /// <param name="MaxJobActive">Time in seconds.</param>
    /// <param name="Timeout">Time in seconds.</param>
    /// <param name="PollInterval">Time in seconds.</param>
    /// <param name="PollingTimeout">Time in seconds.</param>
    public class CamundaCloudWorkerOptions
    {
        public const string ConfigName = "CamundaCloudWorker";

        public int MaxJobActive { get; set; }

        public int Timeout { get; set; }

        public int PollInterval { get; set; }

        public int PollingTimeout { get; set; }
    }
}
