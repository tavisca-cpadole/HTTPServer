using System.Net;

namespace HttpServer
{
    public interface IResponse
    {
        void ResponseGenrator(string page, HttpListenerResponse response);
    }
}