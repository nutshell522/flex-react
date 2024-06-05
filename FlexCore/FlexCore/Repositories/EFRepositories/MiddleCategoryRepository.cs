using FlexCore.Data;
using FlexCore.Models.Entities;
using FlexCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlexCore.Repositories.EFRepositories
{
	public class MiddleCategoryRepository : IMiddleCategoryRepository
	{
		private readonly AppDbContext _db;
		public MiddleCategoryRepository(AppDbContext db)
		{
			_db = db;
		}
		public async Task<IEnumerable<MiddleCategoryEntity>> GetMiddleCategoriesAsync(int TopCategoryId)
		{
			return await _db.MiddleCategories
				.Include(x => x.BottomCategories)
				.Where(x => x.TopCategoryId == TopCategoryId)
				.ToListAsync();
		}
	}
}
