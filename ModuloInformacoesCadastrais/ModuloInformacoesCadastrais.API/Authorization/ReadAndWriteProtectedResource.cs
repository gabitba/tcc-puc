namespace ModuloInformacoesCadastrais.API.Authorization
{
    public class ReadAndWriteProtectedResource
    {
        public string ResourceName { get; private set; }
        public string ResourceSuffix { get; private set; }
        public string Read { get => $"{this.ResourceName}.Read{this.ResourceSuffix}"; }
        public string Write { get => $"{this.ResourceName}.Write{this.ResourceSuffix}"; }

        public ReadAndWriteProtectedResource(string resourceName, string? resourceSuffix = null)
        {
            this.ResourceName = resourceName;
            this.ResourceSuffix = resourceSuffix == null ? string.Empty : (resourceSuffix.StartsWith(".") ? resourceSuffix : $".{resourceSuffix}");
        }
    }
}