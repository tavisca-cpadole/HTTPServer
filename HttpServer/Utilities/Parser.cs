using System;


namespace HttpServer
{
    public class Parser : IParser
    {
        public string BasePath { get; set; }
        public string LocalPath { get; set; }

        public void URLParser(string path)
        {
            Uri uri = new Uri(path);
            BasePath = uri.GetLeftPart(System.UriPartial.Authority);
            LocalPath = uri.AbsolutePath;
        }

        public string ApiParser()
        {
            return LocalPath.Replace("/api/", "");
        }
    }

}
