using System.IO;
using System.Net;


namespace HttpServer
{
    public class Request : IRequest
    {
        public string GetRequestData(HttpListenerContext context)
        {
            var data_text = new StreamReader(context.Request.InputStream,
            context.Request.ContentEncoding).ReadToEnd();
            return System.Web.HttpUtility.UrlDecode(data_text);
        }
    }

}
