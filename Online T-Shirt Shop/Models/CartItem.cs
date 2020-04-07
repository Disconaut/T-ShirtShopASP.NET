using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Online_T_Shirt_Shop.Areas.Identity.Data;

namespace Online_T_Shirt_Shop.Models
{
    public class CartItem
    {
        [Key]
        public string ConsumerId { get; set; }
        
        [Key]
        public int ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
