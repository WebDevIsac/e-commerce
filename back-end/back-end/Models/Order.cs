using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public Cart Cart { get; set; }
        public Customer Customer { get; set; }
    }
}
