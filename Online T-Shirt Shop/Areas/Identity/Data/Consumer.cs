using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Online_T_Shirt_Shop.Areas.Identity.Data.Enums;
using Online_T_Shirt_Shop.Models;

namespace Online_T_Shirt_Shop.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Consumer class
    public class Consumer : IdentityUser
    {
        [MaxLength(256)]
        public string FirstName { get; set; }
        [MaxLength(256)]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(5)")]
        public UserGenders Gender { get; set; }

        public ICollection<Wish> WishList { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
