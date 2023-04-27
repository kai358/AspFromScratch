using System;
namespace AspFromScratch.Models
{
	public interface IPieRepository
	{
		IEnumerable<Pie> AllPies { get; }
		IEnumerable<Pie> PiesOfWeek { get; }
		Pie? GetPieById(int id);
		IEnumerable<Pie> FindPies(string searchQuery);
	}
}

