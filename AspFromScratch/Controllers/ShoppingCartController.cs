using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspFromScratch.Models;
using AspFromScratch.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspFromScratch.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCart shoppingCart;
        private readonly IPieRepository pieRepository;

        public ShoppingCartController(IShoppingCart shoppingCart, IPieRepository pieRepository)
        {
            this.shoppingCart = shoppingCart;
            this.pieRepository = pieRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var shoppingCartItems = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = shoppingCartItems;

            var total = shoppingCart.GetShoppingCartTotal();
            var viewModel = new ShoppingCartViewModel(shoppingCart, total);
            return View(viewModel);
        }
        public RedirectToActionResult AddToShoppingCart(int id)
        {
            var pie = pieRepository.AllPies.FirstOrDefault(x => x.PieId == id);
            if(pie != null)
            {
                shoppingCart.AddToCart(pie);
            }
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveFromShoppingCart(int id)
        {
            var pie = pieRepository.AllPies.FirstOrDefault(x => x.PieId == id);
            if (pie != null)
            {
                shoppingCart.RemoveFromCart(pie);
            }
            return RedirectToAction("Index");
        }
    }
}

