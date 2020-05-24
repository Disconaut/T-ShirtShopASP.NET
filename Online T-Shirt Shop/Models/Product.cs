using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_T_Shirt_Shop.Areas.Identity.Data;
using Online_T_Shirt_Shop.Models.Enums;

namespace Online_T_Shirt_Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [MinLength(3), MaxLength(256), Required] 
        public string Name { get; set; }

        [DataType(DataType.ImageUrl), Required, DisplayName("Image")]
        public string ImagePath { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [EnumDataType(typeof(TShirtSex)), Required]
        public TShirtSex Sex { get; set; }

        [EnumDataType(typeof(TShirtAge)), Required]
        public TShirtAge Age { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(18,2)"), Required]
        public decimal Price { get; set; }

    }
}
