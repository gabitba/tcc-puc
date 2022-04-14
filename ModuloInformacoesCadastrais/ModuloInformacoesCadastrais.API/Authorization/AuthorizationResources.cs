namespace ModuloInformacoesCadastrais.API.Authorization
{
    public static class AuthorizationResources
    {
        public static class Clientes
        {
            private const string RESOURCE_NAME = "Clientes";
            public const string READ_OR_WRITE_POLICY_NAME = "ClientesReadOrWritePolicy";
            public const string WRITE_POLICY_NAME = "ClientesWritePolicy";

            public static readonly ReadAndWriteProtectedResource Role = new ReadAndWriteProtectedResource(RESOURCE_NAME);
        }
    }
}