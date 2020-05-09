using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_T_Shirt_Shop.Models
{
    public class ProductVariant
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int VariantId { get; set; }
        public Variant Variant { get; set; }

        [Required]
        public int InStock { get; set; }
        
        [DataType(DataType.Currency), Column(TypeName = "decimal(18,2)"), Required]
        public decimal Price { get; set; }
    }
}
