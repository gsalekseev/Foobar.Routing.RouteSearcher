using Newtonsoft.Json;

namespace Foobar.Routing.RouteSearcher.Application.Extensions;

public static class ObjectExtensions
{
    public static string ToJson(this object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
}
