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
using Online_T_Shirt_Shop.Models.Enums;

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
            var lastProducts = _shopContext.Products.OrderBy(x => -x.Id).Take(16).Select(x => x);
            return View(lastProducts);
        }

        public IActionResult ShopMan()
        {
            var lastProducts = _shopContext.Products.Where(x => x.Sex == TShirtSex.Man || x.Sex == TShirtSex.Unisex);

            return View(lastProducts);
        }

        public IActionResult ShopWoman()
        {
            var lastProducts = _shopContext.Products.Where(x => x.Sex == TShirtSex.Woman || x.Sex == TShirtSex.Unisex);

            return View(lastProducts);
        }

        public IActionResult ShopKid()
        {
            var lastProducts = _shopContext.Products.Where(x => x.Sex == TShirtSex.Man || x.Sex == TShirtSex.Unisex && x.Age == TShirtAge.Kid);

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
                item.Quantity += quantity ?? 1;
                _shopContext.Update(item);
            }

            _shopContext.SaveChanges();
            return PartialView("_Cart");
        }

        [HttpPost]
        public async Task<IActionResult> EditCart([FromForm(Name = "item.ConsumerId")]string consumerId, [FromForm(Name = "item.ProductId")]int? productId,
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


        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            _shopContext.CartItems.RemoveRange(_shopContext.CartItems.Where(item=>item.ConsumerId == _userManager.GetUserId(User)).Select(x=>x).ToList());
            _shopContext.SaveChanges();
            return RedirectToAction("Index");
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
        public async Task<IActionResult> DeleteFromCart([FromForm(Name = "item.ConsumerId")]string consumerId, [FromForm(Name = "item.ProductId")]int? productId,
            [Bind("ConsumerId, ProductId, Quantity")]
            CartItem item)
        {
            if (consumerId != item.ConsumerId || productId != item.ProductId)
            {
                return NotFound();
            }

            try
            {
                _shopContext.CartItems.Remove(item);
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

    }


}
