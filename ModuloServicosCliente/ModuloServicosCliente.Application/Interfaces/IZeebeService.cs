using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosCliente.Application.Interfaces
{
    public interface IZeebeService
    {
        IJobWorker CriarWorker(string tipoJob, AsyncJobHandler jobHandler, string nomeWorker);

        Task<IProcessInstanceResponse> StartInstanciaProcessoAsync(string idProcesso, IDictionary<string, string> variaveis);

        Task AtualizarWorkflowProcesso(string fileBpmn);
    }
}
