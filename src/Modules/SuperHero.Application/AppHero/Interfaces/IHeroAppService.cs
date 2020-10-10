using SuperHero.Application.AppHero.Input;
using SuperHero.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SuperHero.Application.AppHero.Interfaces
{
    public interface IHeroAppService
    {
        Guid Insert(HeroInput hero);
        Hero GetById(Guid id);
        IEnumerable<Hero> Get();
    }
}
