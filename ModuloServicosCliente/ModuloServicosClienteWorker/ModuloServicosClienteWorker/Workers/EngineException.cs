using System.Runtime.Serialization;

namespace ModuloServicosClienteWorker
{
    [Serializable]
    internal class EngineException : Exception
    {
        private object p;

        public EngineException()
        {
        }

        public EngineException(object p)
        {
            this.p = p;
        }

        public EngineException(string? message) : base(message)
        {
        }

        public EngineException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EngineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}