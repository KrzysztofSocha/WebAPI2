using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI2.Models
{
    public class CreateRestaurantDto
    {
        
        public int restaurantId { get; set; }
        [Required]
        [MaxLength(25)]
        public string name { get; set; }
        public string descryption { get; set; }
        public string category { get; set; }
        public bool hasDelivery { get; set; }

        public string contactEmail { get; set; }
        public string contactNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string city { get; set; }
        [Required]
        [MaxLength(50)]
        public string street { get; set; }

        public string postalCode { get; set; }

    }
}
