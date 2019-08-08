using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;


namespace HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpListener httpListener = new HttpListener();
            HTTPServer hTTPServer = new HTTPServer(httpListener);
            hTTPServer.AddPrefix("http://localhost:8080/");
            hTTPServer.StartServer();
        }
    }

    public class Error {
        public void Error404NotFound(Dispatch dispatch,Response responseGenrate,HttpListenerResponse response,HttpListenerContext context,string path) {
            path = dispatch.Applist["error"] + "404.html";
            responseGenrate.ResponseGenrator(path, response);
            context.Response.Close();
        }
    }

    public class FileSystem
    {
        public bool FileExistsCheck(string physicalPath)
        {
            if (File.Exists(physicalPath))
                return true;
            else
                return false;
        }
    }

    public class Parser {
        public string BasePath { get; set; }
        public string LocalPath { get; set; }

        public void URLParser(string path) {
            Uri uri = new Uri(path);
            BasePath = uri.GetLeftPart(System.UriPartial.Authority);
            LocalPath = uri.AbsolutePath;
        }
    }

    public class Dispatch
    {
        private Dictionary<string, string> applist = new Dictionary<string, string>()
        {
            { "http://localhost:8080","C:/Users/cpadole/Documents/localhost/"},
            { "error","C:/Users/cpadole/Documents/Error/"}
        };

        public Dictionary<string, string> Applist { get => applist; }
    }

    public class WebsiteFactory {
        public IWebsite GetWebsite(string path) {
            if (Directory.Exists(path))
            {
                return new DynamicWebsite();
            }
            else if (File.Exists(path))
            {
                return new StaticWebsite();
            }
            else {
                throw new NotImplementedException();
            }
        }

       
    }

    public interface IWebsite {
         void Handle(Response responseGenrate, HttpListenerContext context, string path,HttpListenerResponse response);
    }

    public class StaticWebsite : IWebsite
    {
        public void Handle(Response responseGenrate,HttpListenerContext context,string path, HttpListenerResponse response)
        {          
            responseGenrate.ResponseGenrator(path, response);
            context.Response.Close();
        }
    }


    public class DynamicWebsite : IWebsite
    {
        public void Handle(Response responseGenrate, HttpListenerContext context, string path, HttpListenerResponse response)
        {
            throw new NotImplementedException();
        }
    }


    public class HTTPServer {

        FileSystem fileSystem = new FileSystem();
        public HttpListener httpListener;
        public HTTPServer(HttpListener httpListener)
        {
            this.httpListener = httpListener;
        }
        public void AddPrefix(string prefix) {
            httpListener.Prefixes.Add(prefix);
        }

        public string StartServer()
        {
            try
            {
                Thread listnerThread = new Thread(new ThreadStart(Run));
                listnerThread.Start();
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
                Response responseGenrate = new Response();
                Parser parser = new Parser();
                Dispatch dispatch = new Dispatch();
                WebsiteFactory websiteFactory = new WebsiteFactory();
                Error error = new Error();
                Request request = new Request();
                parser.URLParser(context.Request.Url.ToString());

                string page = dispatch.Applist[parser.BasePath] + context.Request.Url.LocalPath;
                //Console.WriteLine(page);

                try
                {
                    //if ((parser.LocalPath).ToLowerInvariant() == "/year")
                    //{
                    //    var data_text = new StreamReader(context.Request.InputStream,
                    //    context.Request.ContentEncoding).ReadToEnd();
                    //    var cleaned_data = System.Web.HttpUtility.UrlDecode(data_text);

                    //    JObject currencies = JObject.Parse(cleaned_data);
                    //    var year = currencies.SelectToken("year");
                    //    byte[] buffer = Encoding.UTF8.GetBytes(year.ToString());

                    //    response.ContentLength64 = buffer.Length;
                    //    Stream st = response.OutputStream;
                    //    st.Write(buffer, 0, buffer.Length);
                    //}
                    var cleaned_data = request.GetRequestData(context);
                    if (cleaned_data.Length>0)
                    {
                        JObject jsonObj = JObject.Parse(cleaned_data);
                        //var year = currencies.SelectToken("year");
                        Dictionary<string, string> dictObj = jsonObj.ToObject<Dictionary<string, string>>();
                        string data="";
                        foreach (var item in dictObj) {
                            data+=item.Value;
                        }

                        //byte[] buffer = Encoding.UTF8.GetBytes(data.ToString());

                        //response.ContentLength64 = buffer.Length;
                        //Stream st = response.OutputStream;
                        //st.Write(buffer, 0, buffer.Length);
                        responseGenrate.ResponseGenrator(data,response);
                    }
                    else
                    {
                        IWebsite website = websiteFactory.GetWebsite(page);
                        website.Handle(responseGenrate, context, page, response);
                    }
                }
                catch
                {
                    error.Error404NotFound(dispatch,responseGenrate,response,context,page);
                }
            }
        }
    }

    public class Request
    {
        public string GetRequestData(HttpListenerContext context)
        {
            var data_text = new StreamReader(context.Request.InputStream,
            context.Request.ContentEncoding).ReadToEnd();
            return System.Web.HttpUtility.UrlDecode(data_text);
        }
    }

    public class Response {
        public void ResponseGenrator(string page,HttpListenerResponse response) {

            if (File.Exists(page)) {
                TextReader tr = new StreamReader(page);
                page = tr.ReadToEnd();
                tr.Close();
            }

            byte[] buffer = Encoding.UTF8.GetBytes(page);
            try
            {
                response.ContentLength64 = buffer.Length;
                Stream st = response.OutputStream;
                st.Write(buffer, 0, buffer.Length);
                st.Close();
            }
            catch { }
        }
    }

    public class SystemConfiguration {

    }
}
