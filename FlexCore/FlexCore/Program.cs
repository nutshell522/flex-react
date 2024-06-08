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

// 讀取連接字串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 配置 DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(connectionString));


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITopCategoryService, TopCategoryService>();
builder.Services.AddScoped<ITopCategoryRepository, TopCategoryRepository>();
builder.Services.AddScoped<IMiddleCategoryService, MiddleCategoryService>();
builder.Services.AddScoped<IMiddleCategoryRepository, MiddleCategoryRepository>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(EntityToDtoProfile));
builder.Services.AddAutoMapper(typeof(ProductViewModelProfile));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
		c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
	});
}

//app.UseHttpsRedirection();

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
