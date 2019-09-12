using System.Net;

namespace HttpServer
{
    public interface IError
    {
        void Error404NotFound(IDispatch dispatch, IResponse responseGenrate, HttpListenerResponse response, HttpListenerContext context, string path);
    }
}