using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Online_T_Shirt_Shop.Areas.Identity.Data;
using Online_T_Shirt_Shop.Data;
using Online_T_Shirt_Shop.Extensions;
using Online_T_Shirt_Shop.Models;
using Online_T_Shirt_Shop.Models.Enums;

namespace Online_T_Shirt_Shop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly ShopContext _shopContext;
        private readonly UserManager<Consumer> _userManager;

        public ShopController(ILogger<ShopController> logger, ShopContext shopContext,
            UserManager<Consumer> userManager)
        {
            _logger = logger;
            _shopContext = shopContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var lastProducts = _shopContext.Products.OrderBy(x => -x.Id).Take(16).Select(x => x);
            return View(lastProducts);
        }

        public IActionResult All()
        {
            var lastProducts = _shopContext.Products;

            return View(lastProducts);
        }

        public IActionResult Men()
        {
            var lastProducts = _shopContext.Products.Where(x => x.Sex == TShirtSex.Man || x.Sex == TShirtSex.Unisex);

            return View(lastProducts);
        }

        public IActionResult Women()
        {
            var lastProducts = _shopContext.Products.Where(x => x.Sex == TShirtSex.Woman || x.Sex == TShirtSex.Unisex);

            return View(lastProducts);
        }

        public IActionResult Kids()
        {
            var lastProducts = _shopContext.Products.Where(x => x.Age == TShirtAge.Kid);

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

        public PartialViewResult Cart()
        {
            var userId = _userManager.GetUserId(User);
            IEnumerable<CartItem> items;
            if (userId == null)
            {
                items = HttpContext.Session.Get<Dictionary<int, CartItem>>("cart")?.Values ??
                        Enumerable.Empty<CartItem>();
            }
            else
            {
                items = _shopContext.CartItems.Include(x => x.Product).Where(x => x.ConsumerId == userId);
            }

            return PartialView("_Cart", items);
        }

        public JsonResult CartCounter()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                var cart = HttpContext.Session.Get<Dictionary<int, CartItem>>("cart")?.Values ??
                           Enumerable.Empty<CartItem>();
                return Json(cart.Sum(x => x.Quantity));
            }
            else
            {
                return Json(
                    _shopContext.CartItems.Where(x => x.ConsumerId == userId).Sum(x => x.Quantity));
            }
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
        public async Task<IActionResult> AddToCart(int? productId, int? quantity)
        {
            if (productId == null)
            {
                return BadRequest();
            }

            var product = await _shopContext.Products.FindAsync(productId);
            if (product == null)
            {
                return BadRequest();
            }

            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                var cart = HttpContext.Session.Get<Dictionary<int, CartItem>>("cart") ??
                           new Dictionary<int, CartItem>();
                if (cart.ContainsKey(productId.Value))
                {
                    cart[productId.Value].Quantity += quantity ?? 1;
                }
                else
                {
                    cart.Add(productId.Value, new CartItem
                    {
                        ConsumerId = userId,
                        ProductId = productId.Value,
                        Product = product,
                        Quantity = quantity ?? 1
                    });
                }

                HttpContext.Session.Set("cart", cart);
            }
            else
            {
                var item = await _shopContext.CartItems.FindAsync(userId, productId);

                if (item == null)
                {
                    item = new CartItem
                    {
                        ConsumerId = userId,
                        ProductId = productId.Value,
                        Quantity = quantity ?? 1
                    };

                    _shopContext.Add(item);
                }
                else
                {
                    item.Quantity += quantity ?? 1;
                    _shopContext.Update(item);
                }
            }

            _shopContext.SaveChanges();
            return Ok();
        }

        [HttpPost("EditCart/{productId:int}/{newQuantity:int}")]
        public async Task<ActionResult> EditCart(int? productId, int? newQuantity)
        {
            if (productId == null || newQuantity == null)
            {
                return BadRequest();
            }

            try
            {
                var userId = _userManager.GetUserId(User);
                Product product;
                decimal total;
                decimal productTotal;
                if (userId == null)
                {
                    var cart = HttpContext.Session.Get<Dictionary<int, CartItem>>("cart") ??
                               throw new InvalidOperationException();
                    if (cart.ContainsKey(productId.Value))
                    {
                        var cartItem = cart[productId.Value];
                        cartItem.Quantity = newQuantity.Value;
                        product = cartItem.Product;
                        productTotal = product.Price * cartItem.Quantity;
                        total = cart.Values.Sum(x => x.Quantity * x.Product.Price);
                    }
                    else
                    {
                        throw new KeyNotFoundException();
                    }

                    HttpContext.Session.Set("cart", cart);
                }
                else
                {
                    var item = await _shopContext.CartItems.Include(x => x.Product)
                        .FirstOrDefaultAsync(x => x.ConsumerId == userId && x.ProductId == productId);

                    if (item == null)
                    {
                        throw new KeyNotFoundException();
                    }
                    else
                    {
                        item.Quantity = newQuantity.Value;
                        product = item.Product;
                        productTotal = product.Price * item.Quantity;
                        _shopContext.Update(item);
                        _shopContext.SaveChanges();
                        total = _shopContext.CartItems.Where(x => x.ConsumerId == userId)
                            .Sum(x => x.Quantity * x.Product.Price);
                    }
                }

                return Json(new
                {
                    Total = total.ToString("F"), ProductTotal = productTotal.ToString("F"), Quantity = newQuantity.Value
                });
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        private void ClearCart(string userId)
        {
            if (userId == null)
            {
                HttpContext.Session.Set<Dictionary<int, CartItem>>("cart", null);
            }
            else
            {

                _shopContext.CartItems.RemoveRange(_shopContext.CartItems.Where(item => item.ConsumerId == userId));
                _shopContext.SaveChanges();
            }
        }

        private bool CartItemExists(string consumerId, int? productId)
        {
            return _shopContext.CartItems.Any(item => item.ConsumerId == consumerId && item.ProductId == productId);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }


        [HttpPost("DeleteFromCart/{productId:int}")]
        public async Task<IActionResult> DeleteFromCart(int? productId)
        {
            if (productId == null)
            {
                return BadRequest();
            }

            try
            {
                var userId = _userManager.GetUserId(User);
                decimal total;
                if (userId == null)
                {
                    var cart = HttpContext.Session.Get<Dictionary<int, CartItem>>("cart") ??
                               throw new InvalidOperationException();
                    if (cart.ContainsKey(productId.Value))
                    {
                        cart.Remove(productId.Value);
                        total = cart.Values.Sum(x => x.Quantity * x.Product.Price);
                    }
                    else
                    {
                        throw new KeyNotFoundException();
                    }

                    HttpContext.Session.Set("cart", cart);
                }
                else
                {
                    var item = await _shopContext.CartItems
                        .FindAsync(userId, productId);

                    if (item == null)
                    {
                        throw new KeyNotFoundException();
                    }
                    else
                    {
                        _shopContext.Remove(item);
                        _shopContext.SaveChanges();
                        total = _shopContext.CartItems.Where(x => x.ConsumerId == userId)
                            .Sum(x => x.Quantity * x.Product.Price);
                    }
                }

                return Json(total);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Order()
        {
            var userId = _userManager.GetUserId(User);

            try
            {
                List<OrderProduct> orderProducts;
                decimal total;
                if (userId == null)
                {
                    var cart = HttpContext.Session.Get<Dictionary<int, CartItem>>("cart") ??
                               throw new InvalidOperationException();

                    orderProducts = cart.Values
                        .Select(x => new OrderProduct {ProductId = x.ProductId, Quantity = x.Quantity}).ToList();

                    total = cart.Values.Sum(x => x.Quantity * x.Product.Price);
                }
                else
                {
                    orderProducts = _shopContext.CartItems.Where(x => x.ConsumerId == userId)
                        .Select(x => new OrderProduct {ProductId = x.ProductId, Quantity = x.Quantity}).ToList();

                    total = _shopContext.CartItems.Where(x => x.ConsumerId == userId)
                        .Sum(x => x.Quantity * x.Product.Price);
                }

                var order = new Order()
                {
                    ConsumerId = userId, Date = DateTime.UtcNow, Submission = total, OrderProducts = orderProducts
                };

                _shopContext.Add(order);
                _shopContext.SaveChanges();

                ClearCart(userId);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
    }
}
