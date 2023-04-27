using System;
using AspFromScratch.Models;
using AspFromScratch.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspFromScratch.Components
{
	public class ShoppingCartBin : ViewComponent
	{
		private readonly IShoppingCart shoppingCart;

        public ShoppingCartBin(IShoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var shoppingCartItems = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = shoppingCartItems;

            var total = shoppingCart.GetShoppingCartTotal();
            var viewModel = new ShoppingCartViewModel(shoppingCart, total);
            return View(viewModel);
        }
    }
}

