using System.Collections.Generic;


namespace HttpServer
{
    public class Dispatch : IDispatch
    {
        private Dictionary<string, string> applist = new Dictionary<string, string>()
        {
            { "http://localhost:8080","C:/Users/cpadole/Documents/localhost/"},
            { "error","C:/Users/cpadole/Documents/Error/"}
        };

        public Dictionary<string, string> Applist { get => applist; }
    }

}
