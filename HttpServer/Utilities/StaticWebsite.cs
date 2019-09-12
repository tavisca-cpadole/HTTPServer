using System.Net;


namespace HttpServer
{
    public class StaticWebsite : IWebsite
    {
        public void Handle(IResponse responseGenrate, HttpListenerContext context, string path, HttpListenerResponse response)
        {
            responseGenrate.ResponseGenrator(path, response);
            context.Response.Close();
        }
    }

}
