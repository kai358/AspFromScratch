using System;
using AspFromScratch.Models;

namespace AspFromScratch.ViewModels
{
	public class PieViewModel
	{
		public IEnumerable<Pie> Pies { get; set; }
		public string? CurrentCategory { get; set; }

        public PieViewModel(IEnumerable<Pie> pies, string? currentCategory)
        {
            Pies = pies;
            CurrentCategory = currentCategory;
        }
    }
}

