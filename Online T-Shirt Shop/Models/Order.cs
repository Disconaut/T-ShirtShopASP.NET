using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Online_T_Shirt_Shop.Areas.Identity.Data;

namespace Online_T_Shirt_Shop.Models
{
    public class Order
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime), Required]
        public DateTime Date { get; set; }

        [Required]
        public string ConsumerId { get; set; }
        [ForeignKey("ConsumerId")]
        public Consumer Consumer { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)"), Required]
        public decimal Submission { get; set; }

        [Required]
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
