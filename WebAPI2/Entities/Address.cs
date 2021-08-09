using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI2.Entities
{
    public class Address
    {
        public int addressID { get; set; }

        public string city { get; set; }
        public string street { get; set; }

        public string PostalCode { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
