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

// Add connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Identity and Role management
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders(); // Add default token providers for operations like email verification

// Add service and repository dependencies
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITopCategoryService, TopCategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITopCategoryRepository, TopCategoryRepository>();
builder.Services.AddScoped<IMiddleCategoryService, MiddleCategoryService>();
builder.Services.AddScoped<IMiddleCategoryRepository, MiddleCategoryRepository>();

// Add AutoMapper configuration
builder.Services.AddAutoMapper(typeof(EntityToDtoProfile));
builder.Services.AddAutoMapper(typeof(ProductViewModelProfile));

// Add controller services
builder.Services.AddControllers();

// Configure Swagger/OpenAPI for API documentation
builder.Services.AddEndpointsApiExplorer();

// Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });

    // Define the JWT security scheme
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Input your JWT token in the following format: Bearer {your token}"
    });

    // Make sure swagger UI requires a token for accessing the secured APIs
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:Key")))
    };
});

// Add authorization services
builder.Services.AddAuthorization();

// Configure password options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true; // Require a digit in the password
    options.Password.RequireLowercase = true; // Require a lowercase letter in the password
    options.Password.RequireUppercase = true; // Require an uppercase letter in the password
    options.Password.RequireNonAlphanumeric = false; // Do not require non-alphanumeric characters
    options.Password.RequiredLength = 6; // Require at least 6 characters in the password
});

// Register IEmailSender service
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(c =>
//     {
//         c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//         c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
//     });
// }

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Serve the Swagger UI at the app's root
});

app.UseHttpsRedirection();

app.UseStaticFiles(); // Serve static files

app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.UseAuthentication(); // Enable authentication middleware
app.UseAuthorization(); // Enable authorization middleware

app.MapControllers(); // Map controller routes

// Automatically delete and recreate the database during startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureDeleted(); // Delete the database
    dbContext.Database.EnsureCreated(); // Create the database
    await dbContext.SeedDataAsync(); // Seed initial data
}

app.Run(); // Run the application
