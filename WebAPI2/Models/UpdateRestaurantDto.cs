using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI2.Models
{
    public class UpdateRestaurantDto
    {
       
        [Required]
        [MaxLength(25)]
        public string name { get; set; }
        public string descryption { get; set; }
        public bool hasDelivery { get; set; }
    }
}
