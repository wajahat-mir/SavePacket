using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavePacket.Service;
using SavePacket.ViewModels;
using System.IO;
using System.Text.RegularExpressions;

namespace SavePacket.Helper
{
    class ContainerConfig
    {

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<InterfaceService>().As<IInterfaceService>();
            builder.RegisterType<ApplicationViewModel>();
            builder.RegisterType<MainWindow>();
            builder.RegisterType<DBService>().As<IDBService>();

            var configBuilder = new ConfigurationBuilder();
            configBuilder
                .SetBasePath(GetApplicationRoot())
                .AddJsonFile("appsettings.json");
            var config = configBuilder.Build();

            builder.Register(context => config).As<IConfiguration>();

            builder.Register(
                ctx =>
                {
                    var scope = ctx.Resolve<ILifetimeScope>();
                    return new Mapper(
                    ctx.Resolve<AutoMapper.IConfigurationProvider>(),
                    scope.Resolve);
                })
                .As<IMapper>()
                .InstancePerLifetimeScope();

            var services = new ServiceCollection();
            services.AddLogging();
            builder.Populate(services);

            return builder.Build();
        }

        public static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                              .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }
    }
}
