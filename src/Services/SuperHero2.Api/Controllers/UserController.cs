using System.Threading.Tasks;
using Marraia.Notifications.Base;
using Marraia.Notifications.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperHero.Application.AppUser.Input;
using SuperHero.Application.AppUser.Interfaces;

namespace SuperHero2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserAppService _userAppService;
        public UserController(INotificationHandler<DomainNotification> notification,
                               IUserAppService userAppService)
            : base(notification)
        {
            _userAppService = userAppService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] UserInput input)
        {
            var user = await _userAppService
                                .InsertAsync(input)
                                .ConfigureAwait(false);

            return CreatedContent("", user);
        }
    }
}
