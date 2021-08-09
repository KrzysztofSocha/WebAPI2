using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI2.Entities
{
    public class Dish
    {
        public int Id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public decimal price { get; set; }
        public int  restaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
