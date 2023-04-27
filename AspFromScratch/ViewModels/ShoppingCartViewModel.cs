using System;
using AspFromScratch.Models;

namespace AspFromScratch.ViewModels
{
	public class ShoppingCartViewModel
	{

		public ShoppingCartViewModel(IShoppingCart shoppingCart, decimal total)
		{
			ShoppingCart = shoppingCart;
			TotalAmount = total;
		}
		public IShoppingCart ShoppingCart { get; }
		public decimal TotalAmount { get; }
	}
}

