using SuperHero.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SuperHero.Domain.Interfaces.Repositories
{
    public interface IHeroRepository
    {
        void Insert(Hero hero);
        Hero GetById(Guid id);
        IEnumerable<Hero> Get();
    }
}
