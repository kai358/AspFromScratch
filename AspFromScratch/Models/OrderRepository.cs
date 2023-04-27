using System;
using AspFromScratch.Data;

namespace AspFromScratch.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IShoppingCart shoppingCart;
        private readonly AppDbContext dbContext;

        public OrderRepository(IShoppingCart shoppingCart, AppDbContext dbContext)
        {
            this.shoppingCart = shoppingCart;
            this.dbContext = dbContext;
        }

        public void CreateOrder(Order order)
        {
            order.OrderCreatedAt = DateTime.Now;
            var items = shoppingCart.ShoppingCartItems;
            order.OrderDetails = new();
            order.OrderPrice = shoppingCart.GetShoppingCartTotal();
            foreach(var item in items)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = item.Amount,
                    PieId = item.Pie.PieId,
                    Price = item.Pie.Price
                };
                order.OrderDetails.Add(orderDetail);
            }

            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
        }
    }
}

