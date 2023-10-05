namespace Foobar.Routing.RouteSearcher.Application.Business.Routes.Interfaces
{
    public interface IRouteProviderFactory
    {
        IRouteProvider GetProvider(string name);
    }
}