using System;
using AspFromScratch.Data;
using Microsoft.EntityFrameworkCore;

namespace AspFromScratch.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly AppDbContext appDbContext;

        public ShoppingCart(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public string? ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        public static ShoppingCart GetCard(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            AppDbContext context = services.GetService<AppDbContext>() ?? throw new Exception("Error init");
            var cardId = session?.GetString("CardId") ?? Guid.NewGuid().ToString();
            session?.SetString("CardId", cardId);
            return new ShoppingCart(context){ ShoppingCartId = cardId };
        }

        public void AddToCart(Pie pie)
        {
            var shoppingCartItem = appDbContext.ShoppingCartItems.SingleOrDefault(
                x => x.Pie.PieId == pie.PieId && x.ShoppingCartId == ShoppingCartId
                );
            if (shoppingCartItem == null)
            {
                var shopCartItem = new ShoppingCartItem
                {
                    Pie = pie,
                    ShoppingCartId = ShoppingCartId,
                    Amount = 1,
                };
                appDbContext.ShoppingCartItems.Add(shopCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            appDbContext.SaveChanges();
        }

        public void ClearCart()
        {
            var shoppingCartItems = appDbContext.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId);
            appDbContext.ShoppingCartItems.RemoveRange(shoppingCartItems);
            appDbContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??=
                appDbContext.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Include(x=>x.Pie).ToList();
        }

        public decimal GetShoppingCartTotal()
        {
            return appDbContext.ShoppingCartItems
                .Where(x => x.ShoppingCartId == ShoppingCartId)
                .Select(x => x.Pie.Price * x.Amount).Sum();

        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem = appDbContext.ShoppingCartItems.SingleOrDefault(
              x => x.Pie.PieId == pie.PieId && x.ShoppingCartId == ShoppingCartId
              );
            var result = 0;
            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    result = shoppingCartItem.Amount;
                }
                else
                {
                    appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            appDbContext.SaveChanges();
            return result;
        }
    }
}

