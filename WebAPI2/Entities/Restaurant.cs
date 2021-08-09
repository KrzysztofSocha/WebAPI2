using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebAPI2.Entities
{
    public class Restaurant
    {
        public int restaurantId { get; set; }
        
        public string name { get; set; }
        public string descryption { get; set; }
        public string category { get; set; }
        public bool hasDelivery { get; set; }

        public string contactEmail { get; set; }
        public string contactNumber { get; set; }
        public int addressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Dish> Dishes { get; set; }
    }
}
