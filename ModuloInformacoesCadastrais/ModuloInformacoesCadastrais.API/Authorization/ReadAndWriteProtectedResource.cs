namespace ModuloInformacoesCadastrais.API.Authorization
{
    public class ReadAndWriteProtectedResource
    {
        public string ResourceName { get; private set; }
        public string ResourceSuffix { get; private set; }
        public string Read { get => $"{this.ResourceName}.reader{this.ResourceSuffix}"; }
        public string Write { get => $"{this.ResourceName}.writer{this.ResourceSuffix}"; }

        public ReadAndWriteProtectedResource(string resourceName, string? resourceSuffix = null)
        {
            ResourceName = resourceName;
            ResourceSuffix = resourceSuffix == null ? string.Empty : 
                (resourceSuffix.StartsWith(".") ? resourceSuffix : $".{resourceSuffix}");
        }
    }
}