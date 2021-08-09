using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Models;
using WebAPI2.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NLog.Web;
using Microsoft.Extensions.Logging;

namespace WebAPI2.Services
{
    public interface IRestaurantService
    {
        int CreateRestaurant(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        bool DeleteRestaurant(int id);
        bool UpdateRestaurant(int id, UpdateRestaurantDto dto);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
               .Restaurants
               .Include(r => r.Address)
               .Include(r => r.Dishes)
               .FirstOrDefault(r => r.restaurantId == id);

            if (restaurant is null)
            {
                return null;
            }
            else
            {
                var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
                return restaurantDto;
            }

        }
        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext.Restaurants
               .Include(r => r.Address)
               .Include(r => r.Dishes)
               .ToList();
            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantsDtos;
        }
        public int CreateRestaurant(CreateRestaurantDto dto)
        {

            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.restaurantId;

        }
        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;

        }

        public bool DeleteRestaurant(int id)
        {
            _logger.LogError($"Restaurant with id: {id} DELETE");

            var restaurant = _dbContext
               .Restaurants               
               .FirstOrDefault(r => r.restaurantId == id);
            if (restaurant is null)
            {
                return false;
            }
            else
            {
                _dbContext.Restaurants.Remove(restaurant);
                _dbContext.SaveChanges();
                return true;
            }
        }

        public bool UpdateRestaurant(int id, UpdateRestaurantDto dto)
        {
            var restaurant = _dbContext
               .Restaurants
               .FirstOrDefault(r => r.restaurantId == id);
            if (restaurant is null)
            {
                return false;
            }
            else
            {
                restaurant.name = dto.name;
                restaurant.descryption = dto.descryption;
                restaurant.hasDelivery = dto.hasDelivery;

                
                _dbContext.SaveChanges();
                return true;
            }
        }
    }
}
