using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Entities;
using WebAPI2.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI2.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI2.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        [Authorize(Policy ="HasNationality")]//każdy kto ma wpisaną narodowość ma dostęp do tej akcji
        //polityka zdefiniowana w klasie satrtup
        //[AllowAnonymous]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = _restaurantService.GetAll();
            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);
            _restaurantService.CreateMessage(restaurant);
            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        [Authorize(Roles ="Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {            
            var restaurantId = _restaurantService.CreateRestaurant(dto);
            return Created($"api/resturant/{restaurantId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.DeleteRestaurant(id);  
            return NoContent();            
        }

        [HttpPut("{id}")]
        public ActionResult UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
        {
            _restaurantService.UpdateRestaurant(id, dto);
            return Ok();
        }
    }
}
