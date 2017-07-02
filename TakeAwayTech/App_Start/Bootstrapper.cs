using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using TakeAwayTech.Classes;

namespace TakeAwayTech
{
    public class Bootstrapper
    {
        public static void Run()
        {
            ConfigureAutofac();
        }

        private static void ConfigureAutofac()
        {
            var configuration = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            // Register API controllers using assembly scanning.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<AmountParser>().As<IAmountParser>().InstancePerDependency();

            var container = builder.Build();
            // Set the WebApi dependency resolver.
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;
        }
    }
}