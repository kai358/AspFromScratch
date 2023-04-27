using System;
namespace AspFromScratch.Models
{
	public interface IOrderRepository
	{
		void CreateOrder(Order order);
	}
}

