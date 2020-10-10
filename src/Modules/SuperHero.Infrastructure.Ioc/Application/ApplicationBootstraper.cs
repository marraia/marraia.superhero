using Microsoft.Extensions.DependencyInjection;
using SuperHero.Application.AppHero;
using SuperHero.Application.AppHero.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHero.Infrastructure.Ioc.Application
{
    internal class ApplicationBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IHeroAppService, HeroAppService>();
        }
    }
}
