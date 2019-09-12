using System;
using System.Net;


namespace HttpServer
{
    public class DynamicWebsite : IWebsite
    {
        public void Handle(IResponse responseGenrate, HttpListenerContext context, string path, HttpListenerResponse response)
        {
            throw new NotImplementedException();
        }
    }

}
