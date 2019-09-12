using System.Net;


namespace HttpServer
{
    public class Error : IError
    {
        public void Error404NotFound(IDispatch dispatch, IResponse responseGenrate, HttpListenerResponse response, HttpListenerContext context, string path)
        {
            path = dispatch.Applist["error"] + "404.html";
            responseGenrate.ResponseGenrator(path, response);
            context.Response.Close();
        }
    }

}
