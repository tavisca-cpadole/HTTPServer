using System.Net;

namespace HttpServer
{
    public interface IRequest
    {
        string GetRequestData(HttpListenerContext context);
    }
}