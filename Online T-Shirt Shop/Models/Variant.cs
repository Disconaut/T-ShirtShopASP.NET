using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_T_Shirt_Shop.Models.Enums;

namespace Online_T_Shirt_Shop.Models
{
    public class Variant
    {
        public int Id { get; set; }
        
        [Required]
        public int ColorCode { get; set; }

        [NotMapped]
        public Color Color
        {
            get => Color.FromArgb(ColorCode);
            set => ColorCode = value.ToArgb();
        }

        [EnumDataType(typeof(TShirtSize)), Column(TypeName = "nvarchar(4)"), Required]
        public TShirtSize Size { get; set; }

        public ICollection<ProductVariant> VariantProducts { get; set; }
    }
}
