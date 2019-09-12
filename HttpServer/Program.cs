using Autofac;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;


namespace HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {

            var container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Run();
            }
        }
    }

    //public class WebsiteFactory
    //{
    //    public IWebsite GetWebsite(string path)
    //    {
    //        if (Directory.Exists(path))
    //        {
    //            return new DynamicWebsite();
    //        }
    //        else if (File.Exists(path))
    //        {
    //            return new StaticWebsite();
    //        }
    //        else
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }


    //}


    public class HTTPServer : IHTTPServer
    {

        IParser _parser;
        IWebsite _website;
        IError _error;


        IResponse _responseGenrate;
        IDispatch _dispatch;
        IRequest _request;
        private HttpListener httpListener = new HttpListener();

        public HTTPServer(IParser parser, IWebsite website, IError error, IResponse responseGenrate, IDispatch dispatch, IRequest request)
        {
            _parser = parser;
            _website = website;
            _error = error;

            _responseGenrate = responseGenrate;
            _dispatch = dispatch;
            _request = request;
        }

        public void AddPrefix(string prefix)
        {
            httpListener.Prefixes.Add(prefix);
        }

        public string StartServer()
        {
            try
            {
                //Thread listnerThread = new Thread(new ThreadStart(Run));
                //listnerThread.Start();
                Run();
                return "Server Started";
            }
            catch (WebException e)
            {
                return e.Status.ToString();
            }
        }



        public void Run()
        {
            httpListener.Start();
            while (true)
            {
                HttpListenerContext context = httpListener.GetContext();
                HttpListenerResponse response = context.Response;

                _parser.URLParser(context.Request.Url.ToString());

                if (_parser.LocalPath.Contains("/api/"))
                {
                    RESApi method = new RESApi();

                    string methodName = method.GetMethod(context.Request.HttpMethod, _parser.ApiParser());

                    if (methodName != "No Such Method")
                    {

                        APIOperation aPIOperation = new APIOperation();
                        var cleaned_data = _request.GetRequestData(context);
                        JObject jsonObj = JObject.Parse(cleaned_data);
                        Dictionary<string, string> dictObj = jsonObj.ToObject<Dictionary<string, string>>();
                        string data = "";
                        foreach (var item in dictObj)
                        {
                            data += item.Value;
                        }
                        var output = aPIOperation.GetType().GetMethod(methodName).Invoke(aPIOperation, new object[] { data });
                        _responseGenrate.ResponseGenrator(output.ToString(), response);
                    }
                }
                else
                {
                    string page = _dispatch.Applist[_parser.BasePath] + _parser.LocalPath;

                    //Console.WriteLine(page);

                    try
                    {
                        {
                            _website.Handle(_responseGenrate, context, page, response);
                        }
                    }
                    catch
                    {
                        _error.Error404NotFound(_dispatch, _responseGenrate, response, context, page);
                    }
                }
            }
        }
    }

}
