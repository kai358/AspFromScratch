using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspFromScratch.Migrations;
using AspFromScratch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspFromScratch.Pages
{
	public class CheckoutPageModel : PageModel
    {
        private readonly IShoppingCart shoppingCart;
        private readonly IOrderRepository orderRepository;

        public CheckoutPageModel(IShoppingCart shoppingCart, IOrderRepository orderRepository)
        {
            this.shoppingCart = shoppingCart;
            this.orderRepository = orderRepository;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;
            if (items.Count() == 0)
            {
                ModelState.AddModelError("empty_cart", "Your cart is Empty, add some pies first");
            }
            if (ModelState.IsValid)
            {
                orderRepository.CreateOrder(Order);
                shoppingCart.ClearCart();
                return RedirectToPage("CheckoutCompletePage");
            }
            else
            {
                return Page();
            }
        }

    }
}
