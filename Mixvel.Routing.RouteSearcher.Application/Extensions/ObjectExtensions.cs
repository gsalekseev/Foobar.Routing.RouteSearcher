using Newtonsoft.Json;

namespace Mixvel.Routing.RouteSearcher.Application.Extensions;

public static class ObjectExtensions
{
    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
}
