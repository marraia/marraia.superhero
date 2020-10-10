using SuperHero.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHero.Domain.Interfaces.Repositories
{
    public interface IHeroRepository
    {
        int Insert(Hero hero);
        Task<Hero> GetByIdAsync(int id);
        IEnumerable<Hero> Get();
    }
}
