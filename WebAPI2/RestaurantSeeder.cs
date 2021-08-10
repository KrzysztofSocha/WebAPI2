using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Entities;

namespace WebAPI2
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Users.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }
        }
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    name = "KFC",
                    category = "Fast Food",
                    descryption = "Sieć KFC",
                    contactEmail = "contact@kfc.com",
                    hasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            name = "HotWings",
                            price = 10.30M,
                        },
                        new Dish()
                        {
                            name = "B-Smart",
                            price = 7.5M,
                        },
                    },
                    Address = new Address()
                    {
                        city = "Rzeszów",
                        street = "Rejtana 76",
                        PostalCode = "35-070"
                    },


                },
                new Restaurant()
                {
                    name = "Big City Pizza",
                    category = "Pizza bar",
                    descryption = "Sieć BCZ",
                    contactEmail = "contact@bcz.com",
                    hasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            name = "Salami",
                            price = 20.30M,
                        },
                        new Dish()
                        {
                            name = "Tropic",
                            price = 24.5M,
                        },
                    },
                    Address = new Address()
                    {
                        city = "Rzeszów",
                        street = "Zygmuntowska 7",
                        PostalCode = "35-141"
                    }
                },
                new Restaurant()
                {
                    name = "Nort Fish",
                    category = "Fish bar",
                    descryption = "Restaracja szybkiej obsługi, oferta składa się głównie z ryb i owoców morza",
                    contactEmail = "nf.info@outlook.com",
                    hasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            name = "Krewetki",
                            price = 20.30M,
                        },
                        new Dish()
                        {
                            name = "Morszczuk",
                            price = 24.5M,
                        },
                    },
                    Address = new Address()
                    {
                        city = "Rzeszów",
                        street = "Rejtana 76",
                        PostalCode = "35-070"
                    }
                },
            };
            return restaurants;
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>() {
                new Role()
                {
                    Name="User"
                },
                new Role()
                {
                    Name="Manager"
                },
                new Role()
                {
                    Name="Admin"
                }
            };
            return roles;
        }
    }
}
