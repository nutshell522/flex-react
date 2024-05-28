using Microsoft.EntityFrameworkCore;

namespace FlexCore.Business.Models
{
	public class AppDbContext : DbContext
	{
		// 構造函數，接收 DbContextOptions 以配置 DbContext
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		// DbSet 屬性，表示資料庫中的表
		public DbSet<TopCategory> TopCategories { get; set; }
		public DbSet<MiddleCategory> MiddleCategories { get; set; }
		public DbSet<BottomCategory> BottomCategories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductColor> ProductColors { get; set; }
		public DbSet<ProductSize> ProductSizes { get; set; }
		public DbSet<ColorOption> ColorOptions { get; set; }
		public DbSet<SizeOption> SizeOptions { get; set; }
		public DbSet<ProductPicture> ProductPictures { get; set; }

		// 配置模型之間的關聯
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TopCategory>()
			   .HasMany(t => t.MiddleCategories)
			   .WithOne(m => m.TopCategory)
			   .HasForeignKey(m => m.TopCategoryId);

			modelBuilder.Entity<MiddleCategory>()
				.HasMany(m => m.BottomCategories)
				.WithOne(b => b.MiddleCategory)
				.HasForeignKey(b => b.MiddleCategoryId);

			modelBuilder.Entity<BottomCategory>()
				.HasMany(b => b.Products)
				.WithOne(p => p.BottomCategory)
				.HasForeignKey(p => p.BottomCategoryId);

			modelBuilder.Entity<Product>()
				.HasMany(p => p.ProductColors)
				.WithOne(pc => pc.Product)
				.HasForeignKey(pc => pc.ProductId);

			modelBuilder.Entity<ProductColor>()
				.HasMany(pc => pc.ProductSize)
				.WithOne(ps => ps.ProductColor)
				.HasForeignKey(ps => ps.ProductColorId);

			modelBuilder.Entity<ProductColor>()
				.HasMany(pc => pc.productPictures)
				.WithOne(pp => pp.ProductColor)
				.HasForeignKey(pp => pp.ProductColorId);

			modelBuilder.Entity<ProductSize>()
				.HasOne(ps => ps.SizeOption)
				.WithMany()
				.HasForeignKey(ps => ps.SizeOptionId);

			modelBuilder.Entity<ProductColor>()
				.HasOne(pc => pc.ColorOption)
				.WithMany()
				.HasForeignKey(pc => pc.ColorOptionId);
		}

		// 用於插入初始數據的方法
		public async Task SeedDataAsync()
		{
			if (!await TopCategories.AnyAsync())
			{
				var colorOptions = new List<ColorOption>
		{
			new ColorOption { Name = "黑色", Color="#000000" ,Status=true },
			new ColorOption { Name = "白色", Color="#ffffff" ,Status=true },
			new ColorOption { Name = "紅色", Color="#ff0000" ,Status=true },
			new ColorOption { Name = "綠色", Color="#00ff00" ,Status=true },
			new ColorOption { Name = "藍色", Color="#0000ff" ,Status=true },
			new ColorOption { Name = "黃色", Color="#ffff00" ,Status=true },
			new ColorOption { Name = "紫色", Color="#800080" ,Status=true },
		};
				await ColorOptions.AddRangeAsync(colorOptions);
				await SaveChangesAsync();

				var sizeOptions = new List<SizeOption>
		{
			new SizeOption { Name = "XS", Status=true },
			new SizeOption { Name = "S", Status=true },
			new SizeOption { Name = "M", Status=true },
			new SizeOption { Name = "L", Status=true },
			new SizeOption { Name = "XL", Status=true },
			new SizeOption { Name = "XXL", Status=true },
		};
				await SizeOptions.AddRangeAsync(sizeOptions);
				await SaveChangesAsync();

				var topCategory1 = new TopCategory { Name = "男款", Code = "men" };
				var topCategory2 = new TopCategory { Name = "女款", Code = "women" };
				var topCategory3 = new TopCategory { Name = "兒童款", Code = "kid" };

				await TopCategories.AddRangeAsync(topCategory1, topCategory2, topCategory3);
				await SaveChangesAsync();

				var middleCategories = new List<MiddleCategory>();
				foreach (var item in await TopCategories.ToListAsync())
				{
					var middleCategory1 = new MiddleCategory { Name = "外套", Code = "coat", TopCategoryId = item.Id };
					var middleCategory2 = new MiddleCategory { Name = "上衣", Code = "clothes", TopCategoryId = item.Id };
					var middleCategory3 = new MiddleCategory { Name = "褲子", Code = "pants", TopCategoryId = item.Id };
					var middleCategory4 = new MiddleCategory { Name = "鞋子", Code = "shoes", TopCategoryId = item.Id };

					middleCategories.AddRange(new[] { middleCategory1, middleCategory2, middleCategory3, middleCategory4 });
				}
				await MiddleCategories.AddRangeAsync(middleCategories);
				await SaveChangesAsync();

				var bottomCategories = new List<BottomCategory>();
				foreach (var item in await MiddleCategories.ToListAsync())
				{
					switch (item.Name)
					{
						case "外套":
							bottomCategories.Add(new BottomCategory { Name = "夾克", MiddleCategoryId = item.Id, Code = "jacket" });
							bottomCategories.Add(new BottomCategory { Name = "風衣", MiddleCategoryId = item.Id, Code = "windbreaker" });
							bottomCategories.Add(new BottomCategory { Name = "大衣", MiddleCategoryId = item.Id, Code = "cloak" });
							break;
						case "上衣":
							bottomCategories.Add(new BottomCategory { Name = "T恤", MiddleCategoryId = item.Id, Code = "t-shirt" });
							bottomCategories.Add(new BottomCategory { Name = "襯衫", MiddleCategoryId = item.Id, Code = "shirt" });
							bottomCategories.Add(new BottomCategory { Name = "毛衣", MiddleCategoryId = item.Id, Code = "sweater" });
							break;
						case "褲子":
							bottomCategories.Add(new BottomCategory { Name = "牛仔褲", MiddleCategoryId = item.Id, Code = "jeans" });
							bottomCategories.Add(new BottomCategory { Name = "休閒褲", MiddleCategoryId = item.Id, Code = "casual-pants" });
							bottomCategories.Add(new BottomCategory { Name = "運動褲", MiddleCategoryId = item.Id, Code = "sweatpants" });
							break;
						case "鞋子":
							bottomCategories.Add(new BottomCategory { Name = "運動鞋", MiddleCategoryId = item.Id, Code = "sports-shoes" });
							bottomCategories.Add(new BottomCategory { Name = "休閒鞋", MiddleCategoryId = item.Id, Code = "casual-shoes" });
							bottomCategories.Add(new BottomCategory { Name = "皮鞋", MiddleCategoryId = item.Id, Code = "leather-shoes" });
							break;
					}
				}
				await BottomCategories.AddRangeAsync(bottomCategories);
				await SaveChangesAsync();

				foreach (var item in await BottomCategories.ToListAsync())
				{
					var products = new List<Product>();
					for (int i = 0; i < 30; i++)
					{
						var product = new Product
						{
							Id = $"{item.MiddleCategory.TopCategory.Code}-{item.Code}{i.ToString("000")}",
							Name = $"{item.MiddleCategory.TopCategory.Name}{item.Name}{i}",
							BottomCategoryId = item.Id,
							Description = "清爽，時尚",
							UnitPrice = 1000,
							SalesPrice = 700,
							Status = true
						};
						products.Add(product);
					}
					await Products.AddRangeAsync(products);
				}
				await SaveChangesAsync();

				foreach (var item in await Products.ToListAsync())
				{
					var productColors = new List<ProductColor>();
					foreach (var color in await ColorOptions.ToListAsync())
					{
						var productColor = new ProductColor
						{
							ProductId = item.Id,
							ColorOptionId = color.Id,
						};
						productColors.Add(productColor);
					}
					await ProductColors.AddRangeAsync(productColors);
				}
				await SaveChangesAsync();

				foreach (var item in await ProductColors.ToListAsync())
				{
					var productSizes = new List<ProductSize>();
					foreach (var size in await SizeOptions.ToListAsync())
					{
						var productSize = new ProductSize
						{
							ProductColorId = item.Id,
							SizeOptionId = size.Id,
							Stock = 100
						};
						productSizes.Add(productSize);
					}
					await ProductSizes.AddRangeAsync(productSizes);
				}
				await SaveChangesAsync();
			
		}
		}
	}
}
