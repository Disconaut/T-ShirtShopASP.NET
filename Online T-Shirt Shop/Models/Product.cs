using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online_T_Shirt_Shop.Areas.Identity.Data;
using Online_T_Shirt_Shop.Models.Enums;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace Online_T_Shirt_Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [MinLength(3), MaxLength(256), Required] 
        public string Name { get; set; }

        [DataType(DataType.ImageUrl), Required]
        public string ImagePath { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [EnumDataType(typeof(TShirtSex)), Required]
        public TShirtSex Sex { get; set; }

        [EnumDataType(typeof(TShirtAge)), Required]
        public TShirtAge Age { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(18,2)"), Required]
        public decimal Price { get; set; }

        public string KeywordsJson { get; set; }

        [NotMapped]
        public string[] Keywords
        {
            get => JsonConvert.DeserializeObject<string[]>(KeywordsJson);
            set => KeywordsJson = JsonConvert.SerializeObject(value);
        }

        public ICollection<ProductVariant> ProductVariants { get; set; }

    }
}
