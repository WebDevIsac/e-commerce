using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Controllers;
using back_end.Services;
using back_end.Repositories;

namespace back_end.Models
{
    public class Product
    {
        public int Product_id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }
}
