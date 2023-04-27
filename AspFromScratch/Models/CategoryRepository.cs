using System;
using AspFromScratch.Data;

namespace AspFromScratch.Models
{
	public class CategoryRepository:ICategoryRepository
	{
        private readonly AppDbContext dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Category> AllCategories => dbContext.Categories.OrderBy(x=>x.CategoryName);
    }
}

