using System;
using System.Collections.Generic;

namespace WorldSkills.Models
{
    internal class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public Order()
        {
            Products = new List<Product>();
        }
    }
}
