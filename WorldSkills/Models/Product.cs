using System.Collections.Generic;

namespace WorldSkills.Models
{
    internal class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int Category_Id { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public Product()
        {
            Orders = new List<Order>();
        }
    }
}
