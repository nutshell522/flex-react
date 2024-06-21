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

// 添加服務到容器中
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 配置 DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 配置 Identity 和角色管理服務
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders(); // 添加預設的 Token 生成器，例如電子郵件驗證令牌

// 添加業務服務和存儲庫服務
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITopCategoryService, TopCategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITopCategoryRepository, TopCategoryRepository>();
builder.Services.AddScoped<IMiddleCategoryService, MiddleCategoryService>();
builder.Services.AddScoped<IMiddleCategoryRepository, MiddleCategoryRepository>();

// 添加 AutoMapper 服務
builder.Services.AddAutoMapper(typeof(EntityToDtoProfile));
builder.Services.AddAutoMapper(typeof(ProductViewModelProfile));

// 添加控制器服務
builder.Services.AddControllers();

// 配置 Swagger/OpenAPI，用於 API 文件生成
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 配置 JWT 認證
builder.Services.AddAuthentication(options =>
{
    // 設置默認的身份驗證方案
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // 配置 JWT Bearer 選項
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // 驗證 Issuer
        ValidateAudience = true, // 驗證 Audience
        ValidateLifetime = true, // 驗證 Token 是否過期
        ValidateIssuerSigningKey = true, // 驗證簽名金鑰
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key) // 設置簽名金鑰
    };
});

// 添加授權服務
builder.Services.AddAuthorization();

// 配置密碼選項
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true; // 要求密碼中包含數字
    options.Password.RequireLowercase = true; // 要求密碼中包含小寫字母
    options.Password.RequireUppercase = true; // 要求密碼中包含大寫字母
    options.Password.RequireNonAlphanumeric = false; // 不要求密碼中包含非字母數字字符
    options.Password.RequiredLength = 6; // 要求密碼最少為 6 個字符
});

// 註冊 IEmailSender 服務
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();


// Configure the HTTP request pipeline.
// 配置 HTTP 請求管道
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI(c =>
//	{
//		c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//		c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
//	});
//}

// 配置 HTTP 請求管道
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
});

app.UseHttpsRedirection();

app.UseStaticFiles(); // 確保可以提供靜態文件

app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.UseAuthentication(); // 使用身份驗證中間件
app.UseAuthorization(); // 使用授權中間件

app.MapControllers(); // 映射控制器路由

// 在應用啟動時刪除並重建資料庫
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureDeleted(); // 刪除資料庫
    dbContext.Database.EnsureCreated(); // 重建資料庫
    await dbContext.SeedDataAsync(); // 插入初始數據
}

app.Run(); // 運行應用程式



