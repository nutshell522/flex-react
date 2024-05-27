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
			// TopCategory -> MiddleCategory (一對多)
			modelBuilder.Entity<TopCategory>()
				.HasMany(t => t.MiddleCategories)
				.WithOne(m => m.TopCategory)
				.HasForeignKey(m => m.TopCategoryId);

			// MiddleCategory -> BottomCategory (一對多)
			modelBuilder.Entity<MiddleCategory>()
				.HasMany(m => m.BottomCategories)
				.WithOne(b => b.MiddleCategory)
				.HasForeignKey(b => b.MiddleCategoryId);

			// BottomCategory -> Product (一對多)
			modelBuilder.Entity<BottomCategory>()
				.HasMany(b => b.Products)
				.WithOne(p => p.BottomCategory)
				.HasForeignKey(p => p.BottomCategoryId);

			// Product -> ProductColor (一對多)
			modelBuilder.Entity<Product>()
				.HasMany(p => p.ProductColors)
				.WithOne(pc => pc.Product)
				.HasForeignKey(pc => pc.ProductId);

			// ProductColor -> ProductSize (一對多)
			modelBuilder.Entity<ProductColor>()
				.HasMany(pc => pc.ProductSize)
				.WithOne(ps => ps.ProductColor)
				.HasForeignKey(ps => ps.ProductColorId);

			// ProductColor -> ProductPicture (一對多)
			modelBuilder.Entity<ProductColor>()
				.HasMany(pc => pc.productPictures)
				.WithOne(pp => pp.ProductColor)
				.HasForeignKey(pp => pp.ProductColorId);

			// ProductColor -> ColorOption (多對一)
			modelBuilder.Entity<ProductColor>()
				.HasOne(pc => pc.ColorOption)
				.WithMany()
				.HasForeignKey(pc => pc.ProductId)
				.HasForeignKey(pc => pc.ColorOptionId);


			// ProductSize -> SizeOption (多對一)
			modelBuilder.Entity<ProductSize>()
				.HasOne(ps => ps.SizeOption)
				.WithMany()
				.HasForeignKey(ps => ps.SizeOptionId)
				.HasForeignKey(ps => ps.ProductColorId);
		}

		// 用於插入初始數據的方法
		public void SeedData()
		{
			if (!TopCategories.Any())
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
				ColorOptions.AddRange(colorOptions);
				SaveChanges();

				var sizeOptions = new List<SizeOption>
				{
					new SizeOption { Name = "XS", Status=true },
					new SizeOption { Name = "S", Status=true },
					new SizeOption { Name = "M", Status=true },
					new SizeOption { Name = "L", Status=true },
					new SizeOption { Name = "XL", Status=true },
					new SizeOption { Name = "XXL", Status=true },
				};
				SizeOptions.AddRange(sizeOptions);
				SaveChanges();

				var topCategory1 = new TopCategory { Name = "男款" , Code = "men" };
				var topCategory2 = new TopCategory { Name = "女款" , Code = "women" };
				var topCategory3 = new TopCategory { Name = "兒童款" , Code = "kid" };

				TopCategories.AddRange(topCategory1, topCategory2,topCategory3);
				SaveChanges();

                foreach (var item in TopCategories)
                {

					var middleCategory1 = new MiddleCategory { Name = "外套", Code= "coat", TopCategoryId = item.Id };
					var middleCategory2 = new MiddleCategory { Name = "上衣", Code = "clothes", TopCategoryId = item.Id  };
					var middleCategory3 = new MiddleCategory { Name = "褲子", Code = "pants", TopCategoryId = item.Id };
					var middleCategory4 = new MiddleCategory { Name = "鞋子", Code = "shoes", TopCategoryId = item.Id };

					MiddleCategories.AddRange(middleCategory1, middleCategory2, middleCategory3, middleCategory4);
				}

				SaveChanges();

				foreach (var item in MiddleCategories)
				{
					switch (item.Name)
					{
						case "外套":
							var bottomCategory1 = new BottomCategory { Name = "夾克", MiddleCategoryId = item.Id , Code = "jacket" };
							var bottomCategory2 = new BottomCategory { Name = "風衣", MiddleCategoryId = item.Id , Code = "windbreaker" };
							var bottomCategory3 = new BottomCategory { Name = "大衣", MiddleCategoryId = item.Id , Code = "cloak" };
							BottomCategories.AddRange(bottomCategory1, bottomCategory2, bottomCategory3);
							break;
						case "上衣":
							var bottomCategory4 = new BottomCategory { Name = "T恤", MiddleCategoryId = item.Id , Code = "t-shirt" };
							var bottomCategory5 = new BottomCategory { Name = "襯衫", MiddleCategoryId = item.Id , Code = "shirt" };
							var bottomCategory6 = new BottomCategory { Name = "毛衣", MiddleCategoryId = item.Id , Code = "sweater" };
							BottomCategories.AddRange(bottomCategory4, bottomCategory5, bottomCategory6);
							break;
						case "褲子":
							var bottomCategory7 = new BottomCategory { Name = "牛仔褲", MiddleCategoryId = item.Id ,Code = "jeans"};
							var bottomCategory8 = new BottomCategory { Name = "休閒褲", MiddleCategoryId = item.Id , Code = "casual-pants" };
							var bottomCategory9 = new BottomCategory { Name = "運動褲", MiddleCategoryId = item.Id , Code = "sweatpants" };
							BottomCategories.AddRange(bottomCategory7, bottomCategory8, bottomCategory9);
							break;
						case "鞋子":
							var bottomCategory10 = new BottomCategory { Name = "運動鞋", MiddleCategoryId = item.Id ,Code = "sports-shoes" };
							var bottomCategory11 = new BottomCategory { Name = "休閒鞋", MiddleCategoryId = item.Id ,Code = "casual-shoes" };
							var bottomCategory12 = new BottomCategory { Name = "皮鞋", MiddleCategoryId = item.Id,Code = "leather-shoes" };
							BottomCategories.AddRange(bottomCategory10, bottomCategory11, bottomCategory12);
							break;
					}
				}

				SaveChanges();

				foreach (var item in BottomCategories)
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
					Products.AddRange(products);
				}
				SaveChanges();

				foreach (var item in Products)
				{
					var productColors = new List<ProductColor>();
					foreach (var color in ColorOptions)
					{
						var productColor = new ProductColor
						{
							ProductId = item.Id,
							ColorOptionId = color.Id,
						};
						productColors.Add(productColor);
					}
					ProductColors.AddRange(productColors);
				}
				SaveChanges();

				foreach (var item in ProductColors)
				{
					var productSizes = new List<ProductSize>();
					foreach (var size in SizeOptions)
					{
						var productSize = new ProductSize
						{
							ProductColorId = item.Id,
							SizeOptionId = size.Id,
							Stock = 100
						};
						productSizes.Add(productSize);
					}
					ProductSizes.AddRange(productSizes);
				}
				SaveChanges();
			}
		}
	}
}
