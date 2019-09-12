namespace HttpServer
{
    class Application : IApplication
    {
        private IHTTPServer _hTTPServer;

        public Application(IHTTPServer hTTPServer)
        {
            _hTTPServer = hTTPServer;
        }

        public void Run()
        {
            _hTTPServer.AddPrefix("http://localhost:8080/");
            _hTTPServer.StartServer();
        }
    }
}
