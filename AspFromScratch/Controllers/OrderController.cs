using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspFromScratch.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspFromScratch.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IShoppingCart shoppingCart;

        public OrderController(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            this.orderRepository = orderRepository;
            this.shoppingCart = shoppingCart;
        }

        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;
            if(items.Count()== 0)
            {
                ModelState.AddModelError("empty_cart", "Your cart is Empty, add some pies first");
            }
            if (ModelState.IsValid)
            {
                orderRepository.CreateOrder(order);
                shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            else
            {
                return View(order);
            }
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.Message = "Thanks for your order. You'll soon enjoy our delicious pies!";
            return View();
        }
    }
}

