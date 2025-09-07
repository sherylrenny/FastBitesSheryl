using FastBites.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FastBites.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "CartSession";

        private List<CartItem> GetCart()
        {
            var json = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(json)) return new List<CartItem>();
            return JsonConvert.DeserializeObject<List<CartItem>>(json) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetString(CartSessionKey, JsonConvert.SerializeObject(cart));
        }

        [HttpPost]
        public IActionResult Add(int productId, string name, decimal price, int qty = 1)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item == null)
            {
                cart.Add(new CartItem { ProductId = productId, ProductName = name, UnitPrice = price, Quantity = qty });
            }
            else
            {
                item.Quantity += qty;
            }
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            ViewBag.Total = cart.Sum(i => i.Total);
            return View(cart);
        }

        public IActionResult Remove(int productId)
        {
            var cart = GetCart();
            cart.RemoveAll(i => i.ProductId == productId);
            SaveCart(cart);
            return RedirectToAction("Index");
        }
    }
}
