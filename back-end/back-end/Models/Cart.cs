using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
