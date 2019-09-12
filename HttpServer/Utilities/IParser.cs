namespace HttpServer
{
    public interface IParser
    {
        string BasePath { get; set; }
        string LocalPath { get; set; }

        string ApiParser();
        void URLParser(string path);
    }
}