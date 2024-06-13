using FlexCore.Data;
using FlexCore.Mappings;
using FlexCore.Repositories.EFRepositories;
using FlexCore.Repositories.Interfaces;
using FlexCore.Services.Implementations;
using FlexCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 添加服務到容器中

// 讀取連接字串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 配置 DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(connectionString));

// 添加服務到容器中
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITopCategoryService, TopCategoryService>();
builder.Services.AddScoped<ITopCategoryRepository, TopCategoryRepository>();
builder.Services.AddScoped<IMiddleCategoryService, MiddleCategoryService>();
builder.Services.AddScoped<IMiddleCategoryRepository, MiddleCategoryRepository>();

// 添加 AutoMapper 服務
builder.Services.AddAutoMapper(typeof(EntityToDtoProfile));
builder.Services.AddAutoMapper(typeof(ProductViewModelProfile));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// 配置 Swagger/OpenAPI，用於 API 文件生成
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// 配置 HTTP 請求管道
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
		c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
	});
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // 確保可以提供靜態文件
app.UseAuthorization();

app.UseCors(options =>
{
	options.AllowAnyOrigin()
		   .AllowAnyHeader()
		   .AllowAnyMethod();
});

app.MapControllers();

// 在應用啟動時刪除並重建資料庫
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	dbContext.Database.EnsureDeleted(); // 刪除資料庫
	dbContext.Database.EnsureCreated(); // 重建資料庫
	await dbContext.SeedDataAsync(); // 插入初始數據
}

app.Run();

