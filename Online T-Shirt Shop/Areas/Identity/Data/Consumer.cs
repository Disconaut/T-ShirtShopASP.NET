using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Online_T_Shirt_Shop.Areas.Identity.Data.Enums;

namespace Online_T_Shirt_Shop.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Consumer class
    public class Consumer : IdentityUser
    {
        //public string Username { get; set; }
        //public string FirstName { get; set; }
        //public string SecondName { get; set; }
        //[EnumDataType(typeof(Genders))]
        //public Genders Gender { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime Birthdate { get; set; }
    }
}
