using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Online_T_Shirt_Shop.Models
{
    public class T_Shirt
    {
        public int Id { get; set; }
        [MinLength(3), MaxLength(30)] 
        public string Name { get; set; } = "Product";
        public string ImagePath { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public uint InStock { get; set; }
    }
}
