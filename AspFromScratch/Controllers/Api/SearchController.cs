using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspFromScratch.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspFromScratch.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IPieRepository pieRepository;

        public SearchController(IPieRepository pieRepository)
        {
            this.pieRepository = pieRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(pieRepository.AllPies);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pie = pieRepository.GetPieById(id);
            return pie != null ? Ok(pie) : NotFound();
        }

        [HttpPost]
        public IActionResult Search([FromBody]string searchQuery)
        {

            IEnumerable<Pie> pieList = new List<Pie>();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                pieList = pieRepository.FindPies(searchQuery);
            }
            return new JsonResult(pieList);
        }
    }
}

