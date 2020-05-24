using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Online_T_Shirt_Shop.Data;
using Online_T_Shirt_Shop.Models;

namespace Online_T_Shirt_Shop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly ShopContext _shopContext;

        public ShopController(ILogger<ShopController> logger, ShopContext shopContext)
        {
            _logger = logger;
            _shopContext = shopContext;
        }

        void RenderTable() { }

        public IActionResult Index()
        {
            var lastProducts = _shopContext.Products.OrderBy(x=> -x.Id).Take(16).Select(x => x);
            return View(lastProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Product(int? id)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
