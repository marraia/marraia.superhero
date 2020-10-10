using Marraia.Notifications.Interfaces;
using SuperHero.Application.AppHero.Input;
using SuperHero.Application.AppHero.Interfaces;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperHero.Application.AppHero
{
    public class HeroAppService : IHeroAppService
    {
        private readonly IHeroRepository _heroRepository;
        private readonly ISmartNotification _notification;
        public HeroAppService(ISmartNotification notification, 
            IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
            _notification = notification;
        }

        public IEnumerable<Hero> Get()
        {
            return _heroRepository.Get();
        }

        public async Task<Hero> GetByIdAsync(int id)
        {
            return await _heroRepository
                            .GetByIdAsync(id)
                            .ConfigureAwait(false);
        }

        public async Task<Hero> Insert(HeroInput input)
        {
            //TODO: Criar metodo de obter o editor pelo Id
            var hero = new Hero(input.Name, new Editor(input.IdEditor), input.Age);

            if (!hero.IsValid())
            {
                _notification.NewNotificationBadRequest("Os dados não foram preenchidos corretamente!");
                return default;
            }

            if (!hero.IsMaiority())
            {
                _notification.NewNotificationConflict("O heroi não é maior de idade");
                return default;
            }

            var id = _heroRepository.Insert(hero);
            var heroNew = await GetByIdAsync(id);

            return heroNew;
        }
    }
}
