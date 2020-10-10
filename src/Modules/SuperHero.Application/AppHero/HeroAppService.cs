using SuperHero.Application.AppHero.Input;
using SuperHero.Application.AppHero.Interfaces;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHero.Application.AppHero
{
    public class HeroAppService : IHeroAppService
    {
        private readonly IHeroRepository _heroRepository;
        public HeroAppService(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public IEnumerable<Hero> Get()
        {
            return _heroRepository.Get();
        }

        public Hero GetById(Guid id)
        {
            return _heroRepository.GetById(id);
        }

        public Guid Insert(HeroInput input)
        {
            var hero = new Hero(input.Name, input.Editor);

            if (!hero.IsValid())
                throw new ArgumentException("Os dados obrigatórios não foram preenchidos");

            _heroRepository.Insert(hero);

            return hero.Id;
        }
    }
}
