using FlexCore.Data;
using FlexCore.Extentions;
using FlexCore.Models;
using FlexCore.Models.Entities;
using FlexCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlexCore.Repositories.EFRepositories
{
    /// <summary>
    /// 商品Repository
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 取得商品分頁
        /// </summary>
        /// <param name="pageable"></param>
        /// <param name="topCategoryId"></param>
        /// <param name="middleCategoryId"></param>
        /// <param name="bottomCategoryId"></param>
        /// <param name="maxPrice"></param>
        /// <param name="minPrice"></param>
        /// <returns></returns>
        public async Task<Page<ProductEntity>> GetProductsPageAsync(Pageable pageable, int? topCategoryId, int? middleCategoryId, int? bottomCategoryId,string name ,int? maxPrice, int? minPrice)
        {
            var query = _db.Products
                .Include(p => p.ProductColors)
				.ThenInclude(pc => pc.ColorOption)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.ProductPictures)
                .AsQueryable();

            // 添加分類條件
            query = query.Where(p => !topCategoryId.HasValue || p.BottomCategory.MiddleCategory.TopCategoryId == topCategoryId.Value);
            query = query.Where(p => !middleCategoryId.HasValue || p.BottomCategory.MiddleCategoryId == middleCategoryId.Value);
            query = query.Where(p => !bottomCategoryId.HasValue || p.BottomCategoryId == bottomCategoryId.Value);

            // 添加名稱條件
            query = query.Where(p => string.IsNullOrEmpty(name) || p.Name.Contains(name));

            // 添加價格條件
            query = query.Where(p => !maxPrice.HasValue || p.SalesPrice <= maxPrice.Value);
            query = query.Where(p => !minPrice.HasValue || p.SalesPrice >= minPrice.Value);

            query = query.ApplySorting(pageable.Sort);

            return await query.ApplyPaginationAsync(pageable);
        }

        /// <summary>
        /// 取得商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                .ThenInclude(pc => pc.ProductSizes)
                .ThenInclude(ps => ps.SizeOption)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
