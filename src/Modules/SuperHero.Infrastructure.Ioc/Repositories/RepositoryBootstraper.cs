using Microsoft.Extensions.DependencyInjection;
using SuperHero.Domain.Interfaces.Repositories;
using SuperHero.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHero.Infrastructure.Ioc.Repositories
{
    internal class RepositoryBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IHeroRepository, HeroRepository>();
        }
    }
}
