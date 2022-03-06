using Camunda.Api.Client;
using Camunda.Api.Client.ExternalTask;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace ModuloServicosClienteWorker
{
    public class CadastrarClienteWorker : BackgroundService
    {
        const string WORKER_ID = "worker_1";
        const string TOPIC_NAME = "cadastrar_cliente";
        const long LOCK_TIME = 300000; //5 minutos
        const int MAX_DEGREE_PARALELISM = 2;


        private readonly ILogger<Worker> logger;
        private readonly CamundaClient camundaClient;

        public CadastrarClienteWorker(ILogger<Worker> logger, CamundaClient camundaClient)
        {
            this.logger = logger;
            this.camundaClient = this.camundaClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var fetchingExternalTasks = new FetchExternalTasks
            {
                WorkerId = WORKER_ID,
                MaxTasks = 10,
                UsePriority = true,
                Topics = new List<FetchExternalTaskTopic>()
            };
            fetchingExternalTasks.Topics.Add(new FetchExternalTaskTopic(TOPIC_NAME, LOCK_TIME));
           

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var tasks = await camundaClient.ExternalTasks.FetchAndLock(fetchingExternalTasks);


                ParallelOptions parallelOptions = new()
                {
                    MaxDegreeOfParallelism = MAX_DEGREE_PARALELISM
                };

                await Parallel.ForEachAsync(tasks, parallelOptions, async (externalTask, cancellationToken) =>
                {
                    logger.LogInformation($"Executando task: {externalTask.TopicName} {externalTask.Id}", DateTimeOffset.Now);

                    try
                    {
                        await ExecuteInternal(externalTask, cancellationToken);
                        await CompleteTask(TOPIC_NAME, externalTask.Id);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError($"Erro {externalTask.TopicName} {externalTask.Id}: {ex.Message}", DateTimeOffset.Now);
                        //await TaskError(TOPIC_NAME, externalTask.Id);
                    }
                });

                await Task.Delay(10000, stoppingToken); //10 segundos entre execuções
            }
        }

        private static Task ExecuteInternal(LockedExternalTask externalTask, CancellationToken cancellationToken)
        {
            //TODO call serviço aqui
            return Task.CompletedTask;
        } 

      
        private Task CompleteTask(string topicName, string externalTaskId, Dictionary<string, object> variablesToPassToProcess = null)
        {
            HttpClient http;

            var request = new
            {
                WorkerId = WORKER_ID,
                Variables = ConvertVariables(variablesToPassToProcess)
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(request, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }), Encoding.UTF8, "application/json");
            var response = http.PostAsync("external-task/" + externalTaskId + "/complete", requestContent).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new EngineException("Could not complete external Task: " + response.ReasonPhrase);
            }

            return Task.CompletedTask;
        }


        public static Dictionary<string, Variable> ConvertVariables(Dictionary<string, object> variables)
        {
            // report successful execution
            var result = new Dictionary<string, Variable>();
            if (variables == null)
            {
                return result;
            }
            foreach (var variable in variables)
            {
                Variable camundaVariable = new Variable
                {
                    Value = variable.Value
                };
                result.Add(variable.Key, camundaVariable);
            }
            return result;
        }

        public class Variable
        {
            // lower case to generate JSON we need
            public string Type { get; set; }
            public object Value { get; set; }
            public object ValueInfo { get; set; }
        }
    }

    //private Task TaskError(string topicName, string id)
    //{
    //    camundaClient.
    //}
}