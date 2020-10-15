using Marraia.Notifications.Interfaces;
using NSubstitute;
using SuperHero.Application.AppHero;
using SuperHero.Application.AppHero.Input;
using SuperHero.Domain.Interfaces.Repositories;
using SuperHero.Tests.Comum;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using SuperHero.Domain.Entities;
using Marraia.Notifications.Models;
using MediatR;
using Marraia.Notifications.Handlers;
using Marraia.Notifications;
using System.Linq;
using Marraia.Notifications.Models.Enum;

namespace SuperHero.Tests.AppHero
{
    public class HeroAppServiceTests
    {
        private IHeroRepository subHeroRepository;
        private HeroAppService heroAppService;
        private SmartNotification smartNotification;
        private INotificationHandler<DomainNotification> subNotificationHandler;
        private DomainNotificationHandler domainNotificationHandler;

        public HeroAppServiceTests()
        {
            domainNotificationHandler = new DomainNotificationHandler();
            this.subNotificationHandler = domainNotificationHandler;
            this.subHeroRepository = Substitute.For<IHeroRepository>();
            this.smartNotification = new SmartNotification(this.subNotificationHandler);
            this.heroAppService = new HeroAppService(
                                                this.smartNotification,
                                                this.subHeroRepository);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        [InlineData(5)]
        public void Vaidar_Metodo_Get_Com_Ou_Sem_Dados(int qtd)
        {
            // Arrange
            var listHero = GenerateHeroFaker.CreateListHero(qtd);

            this.subHeroRepository
                    .Get()
                    .Returns(listHero);

            // Act
            var result = this.heroAppService
                                .Get();

            // Assert
            result
                .Should()
                .BeOfType<List<Hero>>();

            result
                .Should()
                .HaveCount(qtd);

            this.subHeroRepository
                    .Received(1)
                    .Get();
        }

        [Fact]
        public async Task Validar_Metodo_GetByID_Com_Dados()
        {
            // Arrange
            var hero = GenerateHeroFaker.CreateHero();
            int id = 10;

            this.subHeroRepository
                .GetByIdAsync(id)
                .Returns(hero);

            // Act
            var result = await this.heroAppService
                                        .GetByIdAsync(id)
                                        .ConfigureAwait(false);

            // Assert
            result
                .Should()
                .BeOfType<Hero>();

            result.Id.Should().NotBe(0);
            result.Name.Should().Be(hero.Name);
            result.Editor.Id.Should().NotBe(0);
            result.Editor.Name.Should().Be(hero.Editor.Name);
            result.Age.Should().Be(hero.Age);

            await this.subHeroRepository
                    .Received(1)
                    .GetByIdAsync(Arg.Any<int>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task Validar_Metodo_GetByID_Sem_Dados()
        {
            // Arrange
            var hero = default(Hero);
            int id = 10;

            this.subHeroRepository
                .GetByIdAsync(id)
                .Returns(hero);

            // Act
            var result = await this.heroAppService
                                        .GetByIdAsync(id)
                                        .ConfigureAwait(false);

            // Assert
            result
                .Should()
                .BeNull();

            await this.subHeroRepository
                    .Received(1)
                    .GetByIdAsync(Arg.Any<int>())
                    .ConfigureAwait(false);
        }

        [Fact]
        public async Task Validar_Metodo_Insert_Sem_Dados_Obrigatorios()
        {
            // Arrange
            var input = new HeroInput();

            // Act
            var result = await this
                                .heroAppService
                                .Insert(input)
                                .ConfigureAwait(false);

            // Assert
            result
                .Should()
                .Be(default(Hero));

            domainNotificationHandler
                .GetNotifications()
                .Should()
                .HaveCount(1);


            domainNotificationHandler
                .GetNotifications()
                .FirstOrDefault()
                .DomainNotificationType
                .Should()
                .Be(DomainNotificationType.BadRequest);

            domainNotificationHandler
                .GetNotifications()
                .FirstOrDefault()
                .Value
                .Should()
                .Be("Os dados não foram preenchidos corretamente!");
        }

        [Theory]
        [InlineData(17)]
        [InlineData(12)]
        [InlineData(0)]
        public async Task Validar_Metodo_Insert_Hero_Menor_de_Idade(int age)
        {
            // Arrange
            var input = GenerateHeroFaker.CreateHeroInput(age);

            // Act
            var result = await this
                                .heroAppService
                                .Insert(input)
                                .ConfigureAwait(false);

            // Assert
            result
                .Should()
                .Be(default(Hero));

            domainNotificationHandler
                .GetNotifications()
                .Should()
                .HaveCount(1);

            domainNotificationHandler
                .GetNotifications()
                .FirstOrDefault()
                .DomainNotificationType
                .Should()
                .Be(DomainNotificationType.Conflict);

            domainNotificationHandler
                .GetNotifications()
                .FirstOrDefault()
                .Value
                .Should()
                .Be("O heroi não é maior de idade");
        }

        [Theory]
        [InlineData(18)]
        [InlineData(20)]
        [InlineData(100)]
        public async Task Validar_Metodo_Insert_Com_Sucesso(int age)
        {
            // Arrange
            var input = GenerateHeroFaker.CreateHeroInput(age);
            var hero = GenerateHeroFaker.CreateHero(input.Name, input.IdEditor, input.Age);

            this.subHeroRepository
                .GetByIdAsync(Arg.Any<int>())
                .Returns(hero);

            this.subHeroRepository
                .Insert(hero)
                .Returns(hero.Id);

            // Act
            var result = await this
                                .heroAppService
                                .Insert(input)
                                .ConfigureAwait(false);

            // Assert
            result
                .Should()
                .BeOfType<Hero>();

            result.Id.Should().Be(hero.Id);
            result.Name.Should().Be(hero.Name);
            result.Editor.Id.Should().Be(hero.Editor.Id);
            result.Editor.Name.Should().Be(hero.Editor.Name);
            result.Age.Should().Be(age);

            await this.subHeroRepository
                        .Received(1)
                        .GetByIdAsync(Arg.Any<int>())
                        .ConfigureAwait(false);

             this.subHeroRepository
                    .Received(1)
                    .Insert(Arg.Any<Hero>());
        }
    }
}
