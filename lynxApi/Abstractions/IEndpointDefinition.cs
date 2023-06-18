namespace lynxApi.Abstractions
{
    public interface IEndpointDefinition
    {
        void RegisterEndpoints(WebApplication app);
    }
}
