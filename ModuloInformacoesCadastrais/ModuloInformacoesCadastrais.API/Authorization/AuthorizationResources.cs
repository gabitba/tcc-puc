namespace ModuloInformacoesCadastrais.API.Authorization
{
    public static class AuthorizationResources
    {
        private const string ALL_SUFFIX = ".All";
        public static class Client
        {
            private const string CLIENT_PREFIX = "Client";
            public const string READ_OR_WRITE_POLICY_NAME = "ClientReadOrWritePolicy";
            public const string WRITE_POLICY_NAME = "ClientWritePolicy";

            public static readonly ReadAndWriteProtectedResource Scope = new ReadAndWriteProtectedResource(CLIENT_PREFIX);
            public static readonly ReadAndWriteProtectedResource Role = new ReadAndWriteProtectedResource(CLIENT_PREFIX, ALL_SUFFIX);
        }
    }
}