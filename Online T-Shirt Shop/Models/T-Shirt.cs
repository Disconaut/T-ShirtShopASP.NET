using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_T_Shirt_Shop.Models
{
    public class T_Shirt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
    }
}
