using Microsoft.Extensions.DependencyInjection;
using SuperHero.Infrastructure.Ioc.Application;
using SuperHero.Infrastructure.Ioc.Repositories;
using System;

namespace SuperHero.Infrastructure.Ioc
{
    public class RootBootstrapper
    {
        public void RootRegisterServices(IServiceCollection services)
        {
            new ApplicationBootstraper().ChildServiceRegister(services);
            new RepositoryBootstraper().ChildServiceRegister(services);
        }
    }
}
