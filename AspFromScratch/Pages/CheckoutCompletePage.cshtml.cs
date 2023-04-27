using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspFromScratch.Pages
{
	public class CheckoutCompletePageModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Message"] = "Thanks for your order. You'll soon enjoy our delicious pies!";
        }
    }
}
