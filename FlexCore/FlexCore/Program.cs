using FlexCore.Data;
using FlexCore.Mappings;
using FlexCore.Repositories.EFRepositories;
using FlexCore.Repositories.Interfaces;
using FlexCore.Services.Implementations;
using FlexCore.Services.Interfaces;
using FlexCore.Utils.EmailSender;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add User Secrets
builder.Configuration.AddUserSecrets<Program>();

// �K�[�A�Ȩ�e����
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// �t�m DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// �t�m Identity �M����޲z�A��
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders(); // �K�[�w�]�� Token �ͦ����A�Ҧp�q�l�l�����ҥO�P

// �K�[�~�ȪA�ȩM�s�x�w�A��
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITopCategoryService, TopCategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITopCategoryRepository, TopCategoryRepository>();
builder.Services.AddScoped<IMiddleCategoryService, MiddleCategoryService>();
builder.Services.AddScoped<IMiddleCategoryRepository, MiddleCategoryRepository>();

// �K�[ AutoMapper �A��
builder.Services.AddAutoMapper(typeof(EntityToDtoProfile));
builder.Services.AddAutoMapper(typeof(ProductViewModelProfile));

// �K�[����A��
builder.Services.AddControllers();

// �t�m Swagger/OpenAPI�A�Ω� API ���ͦ�
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// �t�m JWT �{��
builder.Services.AddAuthentication(options =>
{
    // �]�m�q�{���������Ҥ��
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // �t�m JWT Bearer �ﶵ
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // ���� Issuer
        ValidateAudience = true, // ���� Audience
        ValidateLifetime = true, // ���� Token �O�_�L��
        ValidateIssuerSigningKey = true, // ����ñ�W���_
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key) // �]�mñ�W���_
    };
});

// �K�[���v�A��
builder.Services.AddAuthorization();

// �t�m�K�X�ﶵ
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true; // �n�D�K�X���]�t�Ʀr
    options.Password.RequireLowercase = true; // �n�D�K�X���]�t�p�g�r��
    options.Password.RequireUppercase = true; // �n�D�K�X���]�t�j�g�r��
    options.Password.RequireNonAlphanumeric = false; // ���n�D�K�X���]�t�D�r���Ʀr�r��
    options.Password.RequiredLength = 6; // �n�D�K�X�̤֬� 6 �Ӧr��
});

// ���U IEmailSender �A��
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();


// Configure the HTTP request pipeline.
// �t�m HTTP �ШD�޹D
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI(c =>
//	{
//		c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//		c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
//	});
//}

// �t�m HTTP �ШD�޹D
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
});

app.UseHttpsRedirection();

app.UseStaticFiles(); // �T�O�i�H�����R�A���

app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.UseAuthentication(); // �ϥΨ������Ҥ�����
app.UseAuthorization(); // �ϥα��v������

app.MapControllers(); // �M�g�������

// �b���αҰʮɧR���í��ظ�Ʈw
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureDeleted(); // �R����Ʈw
    dbContext.Database.EnsureCreated(); // ���ظ�Ʈw
    await dbContext.SeedDataAsync(); // ���J��l�ƾ�
}

app.Run(); // �B�����ε{��



