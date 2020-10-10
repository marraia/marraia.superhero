using SuperHero.Domain.Entities;
using SuperHero.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperHero.Infrastructure.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private List<Hero> heroes;
        public HeroRepository()
        {
            heroes = new List<Hero>();
        }
        
        public IEnumerable<Hero> Get()
        {
            return heroes.ToList();
        }

        public Hero GetById(Guid id)
        {
            return heroes
                    .Where(filter => filter.Id == id)
                    .FirstOrDefault();
        }

        public void Insert(Hero hero)
        {
            heroes.Add(hero);
        }
    }
}
