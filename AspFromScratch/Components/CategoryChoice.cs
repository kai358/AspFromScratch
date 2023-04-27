using System;
using AspFromScratch.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspFromScratch.Components
{
	public class CategoryChoice:ViewComponent
	{
		private readonly ICategoryRepository categoryRepository;

        public CategoryChoice(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = categoryRepository.AllCategories;
            return View(categories);
        }
    }
}

