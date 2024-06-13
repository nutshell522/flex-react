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
// �K�[�A�Ȩ�e����

// Ū���s���r��
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// �t�m DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(connectionString));

// �K�[�A�Ȩ�e����
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITopCategoryService, TopCategoryService>();
builder.Services.AddScoped<ITopCategoryRepository, TopCategoryRepository>();
builder.Services.AddScoped<IMiddleCategoryService, MiddleCategoryService>();
builder.Services.AddScoped<IMiddleCategoryRepository, MiddleCategoryRepository>();

// �K�[ AutoMapper �A��
builder.Services.AddAutoMapper(typeof(EntityToDtoProfile));
builder.Services.AddAutoMapper(typeof(ProductViewModelProfile));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// �t�m Swagger/OpenAPI�A�Ω� API ���ͦ�
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// �t�m HTTP �ШD�޹D
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

app.UseStaticFiles(); // �T�O�i�H�����R�A���
app.UseAuthorization();

app.UseCors(options =>
{
	options.AllowAnyOrigin()
		   .AllowAnyHeader()
		   .AllowAnyMethod();
});

app.MapControllers();

// �b���αҰʮɧR���í��ظ�Ʈw
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	dbContext.Database.EnsureDeleted(); // �R����Ʈw
	dbContext.Database.EnsureCreated(); // ���ظ�Ʈw
	await dbContext.SeedDataAsync(); // ���J��l�ƾ�
}

app.Run();

