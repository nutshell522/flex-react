using FlexCore.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 讀取連接字串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 配置 DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// 在應用啟動時刪除並重建資料庫
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	dbContext.Database.EnsureDeleted(); // 刪除資料庫
	dbContext.Database.EnsureCreated(); // 重建資料庫
	dbContext.SeedData(); // 插入初始數據
}

app.Run();
