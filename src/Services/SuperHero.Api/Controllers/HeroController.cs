using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHero.Application.AppHero.Input;
using SuperHero.Application.AppHero.Interfaces;

namespace SuperHero.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IHeroAppService _heroAppService;
        public HeroController(IHeroAppService heroAppService)
        {
            _heroAppService = heroAppService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] HeroInput input)
        {
            try
            {
                var item = _heroAppService.Insert(input);
                return Created("", item);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Erro => {ex.Message}");
            }
        }

        [HttpGet] //api/hero
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            return Ok(_heroAppService.Get());
        }

        [HttpGet] //api/hero/id
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Get([FromRoute] Guid id)
        {
            return Ok(_heroAppService.GetById(id));
        }
    }
}
