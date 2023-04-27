using System;
using AspFromScratch.Data;
using Microsoft.EntityFrameworkCore;

namespace AspFromScratch.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext dbContext;

        public PieRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Pie> AllPies => dbContext.Pies.Include(x=>x.Category);

        public IEnumerable<Pie> PiesOfWeek => dbContext.Pies.Include(x=>x.Category).Where(x => x.IsPieOfTheWeek);

        public IEnumerable<Pie> FindPies(string searchQuery)
        {
            return dbContext.Pies.Where(x => x.Name.Contains(searchQuery));
        }

        public Pie? GetPieById(int id)
        {
            return dbContext.Pies.SingleOrDefault(x => x.PieId == id);
        }
    }
}

