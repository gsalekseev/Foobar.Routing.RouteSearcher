using System.Security.Cryptography;
using System.Text;
using Mixvel.Routing.RouteSearcher.Application.Extensions;

namespace Mixvel.Routing.RouteSearcher.Application.Common.Helpers;

public class Md5Helper
{
    public static Guid GetMd5AsGuid(object obj)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(obj.ToJson());
        var hash = md5.ComputeHash(inputBytes);
        return new Guid(hash);
    }
}
