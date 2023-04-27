using System;
namespace AspFromScratch.Models
{
	public interface ICategoryRepository
	{
		IEnumerable<Category> AllCategories { get; }
	}
}

