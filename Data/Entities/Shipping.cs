using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingCart.Data
{
    public class Shipping
    {
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? Wards { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public string? DeliveryType { get; set; }

        public DateTime CreateAt { get; private set; }

        public Shipping()
        {
            CreateAt = DateTime.Now;
        }

    }
}