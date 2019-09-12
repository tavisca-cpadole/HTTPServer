using System.Collections.Generic;

namespace HttpServer
{
    public interface IDispatch
    {
        Dictionary<string, string> Applist { get; }
    }
}