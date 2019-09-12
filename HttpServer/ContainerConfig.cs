using Autofac;

namespace HttpServer
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            //main app
            builder.RegisterType<Application>().As<IApplication>();

            builder.RegisterType<HTTPServer>().As<IHTTPServer>();

            //Data Processor
            builder.RegisterType<Dispatch>().As<IDispatch>();
            builder.RegisterType<Request>().As<IRequest>();
            builder.RegisterType<Response>().As<IResponse>();


            //utilities
            builder.RegisterType<StaticWebsite>().As<IWebsite>();
            builder.RegisterType<Error>().As<IError>();
            builder.RegisterType<Parser>().As<IParser>();

            return builder.Build();
        }
    }
}
