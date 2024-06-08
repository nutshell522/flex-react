using FlexCore.Data;
using FlexCore.Models.Entities;
using FlexCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlexCore.Repositories.EFRepositories
{
	public class TopCategoryRepository : ITopCategoryRepository
	{
		private readonly AppDbContext _db;
		public TopCategoryRepository(AppDbContext db)
		{
			_db = db;
		}
		public async Task<IEnumerable<TopCategoryEntity>> GetTopCategoriesAsync()
		{
			return await _db.TopCategories
				.Include(tc => tc.MiddleCategories)
				.ThenInclude(mc => mc.BottomCategories)
				.ToListAsync();
		}
	}
}
