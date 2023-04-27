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
    public class PieController : Controller
    {
        private readonly IPieRepository _pieReposetory;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(ICategoryRepository categoryRepository, IPieRepository pieRepository)
        {
            _categoryRepository = categoryRepository;
            _pieReposetory = pieRepository;
        }

        //public IActionResult List()
        //{
         //   var pieViewModel = new PieViewModel(_pieReposetory.AllPies, "All Pies");
          //  return View(pieViewModel);
       // }
        public IActionResult Details(int id)
        {
            var pie = _pieReposetory.GetPieById(id);
            if(pie == null)
            {
                return NotFound();
            }
            return View(pie);
        }
        public IActionResult List(string category)
        {
            IEnumerable<Pie> pies;
            string? currentCategory = default;
            if(string.IsNullOrEmpty(category))
            {
                pies = _pieReposetory.AllPies.OrderBy(x => x.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies =_pieReposetory.AllPies.Where(c=>c.Category.CategoryName == category).OrderBy(x=>x.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(x=>x.CategoryName==category)?.CategoryName;
            }
            return View(new PieViewModel(pies,currentCategory));
        }
    }
}

