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

namespace WebAPI2.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {

            var restaurantsDtos = _restaurantService.GetAll();
            return Ok(restaurantsDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);

            if (restaurant is null)
            {
                return NotFound();
            }
            else
            {

                return Ok(restaurant);
            }
        }
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var restaurantId = _restaurantService.CreateRestaurant(dto);
            return Created($"api/resturant/{restaurantId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var checkDelete =_restaurantService.DeleteRestaurant(id);
            if (!checkDelete)
            {
                return NotFound();
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPut("{id}")]
        public ActionResult UpdateRestaurant ([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var checkUpdate = _restaurantService.UpdateRestaurant(id, dto);
            if (!checkUpdate)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
            
            
        }
    }
}
