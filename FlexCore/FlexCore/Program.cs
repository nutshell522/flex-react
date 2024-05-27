using FlexCore.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Ū���s���r��
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// �t�m DbContext
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


// �b���αҰʮɧR���í��ظ�Ʈw
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	dbContext.Database.EnsureDeleted(); // �R����Ʈw
	dbContext.Database.EnsureCreated(); // ���ظ�Ʈw
	dbContext.SeedData(); // ���J��l�ƾ�
}

app.Run();
