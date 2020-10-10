using SuperHero.Application.AppHero.Input;
using SuperHero.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHero.Application.AppHero.Interfaces
{
    public interface IHeroAppService
    {
        Task<Hero> Insert(HeroInput hero);
        Task<Hero> GetByIdAsync(int id);
        IEnumerable<Hero> Get();
    }
}
