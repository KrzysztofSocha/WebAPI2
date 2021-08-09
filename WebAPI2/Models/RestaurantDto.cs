using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI2.Models
{
    public class RestaurantDto
    {
        public int restaurantId { get; set; }

        public string name { get; set; }
        public string descryption { get; set; }
        public string category { get; set; }
        public bool hasDelivery { get; set; }

        public string city { get; set; }
        public string street { get; set; }

        public string postalCode { get; set; }

        public List<DishDto> Dishes { get; set; }


    }
}
