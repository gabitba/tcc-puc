using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace ModuloServicosClienteWorker.Infra.Services
{
    public interface ICamundaService
    {
        IJobWorker CriarWorker(string tipoJob, AsyncJobHandler jobHandler, string nomeWorker);
        Task<IProcessInstanceResponse> ComecarInstanciaProcesso(string idProcesso, IDictionary<object, object> variaveis);
    }
}
