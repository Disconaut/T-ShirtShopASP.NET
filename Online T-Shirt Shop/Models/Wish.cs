using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_T_Shirt_Shop.Models
{
    public class Wish
    {
        [Key]
        public int ConsumerId { get; set; }
        [Key]
        public int ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; }
    }
}
