using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Online_T_Shirt_Shop.Models;
using Online_T_Shirt_Shop.Models.Enums;

namespace Online_T_Shirt_Shop.Areas.Admin.Models.ViewModels
{

    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        [DataType(DataType.MultilineText)] public string Description { get; set; }


        [Required(ErrorMessage = "Sex is required")]
        public TShirtSex Sex { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public TShirtAge Age { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }


    }

}
