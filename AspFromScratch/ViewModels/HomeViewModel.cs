using System;
using AspFromScratch.Models;

namespace AspFromScratch.ViewModels
{
	public class HomeViewModel
	{
        public IEnumerable<Pie> PiesOfTheWeek { get; set; }

        public HomeViewModel(IEnumerable<Pie> piesOfTheWeek)
        {
            PiesOfTheWeek = piesOfTheWeek;
        }
    }
}

