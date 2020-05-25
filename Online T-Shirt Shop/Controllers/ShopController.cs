using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Online_T_Shirt_Shop.Areas.Identity.Data;
using Online_T_Shirt_Shop.Data;
using Online_T_Shirt_Shop.Models;

namespace Online_T_Shirt_Shop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly ShopContext _shopContext;
        private readonly UserManager<Consumer> _userManager;

        public ShopController(ILogger<ShopController> logger, ShopContext shopContext, UserManager<Consumer> userManager)
        {
            _logger = logger;
            _shopContext = shopContext;
            _userManager = userManager;
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

        public async Task<IActionResult> Product(int? id)
        {
            if (id == null || !_shopContext.Products.Any(x => x.Id == id))
            {
                return NotFound();
            }
            return View(await _shopContext.Products.FindAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int? id, int? quantity, [Bind("Id")] Product product)
        {
            if (product.Id != id)
            {
                return NotFound();
            }

            var item = await _shopContext.CartItems.FindAsync(_userManager.GetUserId(User), product.Id);
            if (item == null)
            {
                item = new CartItem
                {
                    ConsumerId = _userManager.GetUserId(User),
                    ProductId = product.Id,
                    Quantity = quantity ?? 1
                };

                _shopContext.Add(item);
            }
            else
            {
                item.Quantity += quantity??1;
                _shopContext.Update(item);
            }

            _shopContext.SaveChanges();
            return PartialView("_Cart");
        }

        [HttpPost]
        public async Task<IActionResult> EditCart([FromForm(Name = "item.ConsumerId")]string consumerId, [FromForm(Name = "item.ProductId")]int? productId, [FromForm(Name = "sign")]string sign,
            [Bind("ConsumerId, ProductId, Quantity")]
            CartItem item)
        {
            if (consumerId != item.ConsumerId || productId != item.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (sign == "plus")
                    {
                        item.Quantity += 1;
                    }else if (sign == "minus")
                    {
                        item.Quantity -= 1;
                    }
                    _shopContext.Update(item);
                    await _shopContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(item.ConsumerId, item.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return PartialView("_Cart");
            }
            return Error();
        }

        private bool CartItemExists(string consumerId, int? productId)
        {
            return _shopContext.CartItems.Any(item => item.ConsumerId == consumerId && item.ProductId == productId);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public async Task<int> DeleteFromCard(string consumerId, int? productId, string action,
            [Bind("ConsumerId, ProductId, Quantity")]
            CartItem item)
        {
            if (consumerId != item.ConsumerId || productId != item.ProductId)
            {
                return -1;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _shopContext.Remove(item);
                    await _shopContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(item.ConsumerId, item.ProductId))
                    {
                        return -1;
                    }
                    else
                    {
                        throw;
                    }
                }
                return item.Quantity;
            }
            return -1;
        }

    }


}
