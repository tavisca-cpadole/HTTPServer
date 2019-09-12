namespace HttpServer
{
    public interface IHTTPServer
    {
        void AddPrefix(string prefix);
        void Run();
        string StartServer();
    }
}