using System.Net;


namespace HttpServer
{
    public interface IWebsite
    {
        void Handle(IResponse responseGenrate, HttpListenerContext context, string path, HttpListenerResponse response);
    }

}
