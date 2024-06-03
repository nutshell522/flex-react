using FlexCore.Data;
using FlexCore.Extentions;
using FlexCore.Models;
using FlexCore.Models.Entities;
using FlexCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlexCore.Repositories.EFRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Page<ProductEntity>> GetProductsPageAsync(Pageable pageable, int? topCategoryId, int? middleCategoryId, int? bottomCategoryId, int? maxPrice, int? minPrice)
        {
            var query = _db.Products
                .Include(p => p.BottomCategory)
                .ThenInclude(bc => bc.MiddleCategory)
                .ThenInclude(mc => mc.TopCategory)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.ColorOption)
                .AsQueryable();

            // 添加分類條件
            query = query.Where(p => !topCategoryId.HasValue || p.BottomCategory.MiddleCategory.TopCategoryId == topCategoryId.Value);
            query = query.Where(p => !middleCategoryId.HasValue || p.BottomCategory.MiddleCategoryId == middleCategoryId.Value);
            query = query.Where(p => !bottomCategoryId.HasValue || p.BottomCategoryId == bottomCategoryId.Value);

            // 添加價格條件
            query = query.Where(p => !maxPrice.HasValue || p.SalesPrice <= maxPrice.Value);
            query = query.Where(p => !minPrice.HasValue || p.SalesPrice >= minPrice.Value);

            query = query.ApplySorting(pageable.Sort);

            return await query.ApplyPaginationAsync(pageable);
        }

        public async Task<ProductEntity> GetProductByIdAsync(string id)
        {
            return await _db.Products
                .Include(p => p.BottomCategory)
                .ThenInclude(bc => bc.MiddleCategory)
                .ThenInclude(mc => mc.TopCategory)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.ColorOption)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.ProductPictures)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.ProductSize)
                .ThenInclude(ps => ps.SizeOption)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
