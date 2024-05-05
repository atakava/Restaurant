using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using Restaurant.DAL;
using Restaurant.DAL.Interfaces;
using Restaurant.DAL.Repository;
using Restaurant.Service.Implementations;
using Restaurant.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddCookie(option =>
{
    option.Cookie.Name = "user";
    option.Cookie.SecurePolicy = CookieSecurePolicy.None;
    option.Events.OnValidatePrincipal = async context => { };
});

builder.Services.AddAuthorization(option =>
{
    const string defaultPolicy = "defaultPolicy";
    option.AddPolicy(defaultPolicy, policyBuilder => { policyBuilder.RequireClaim(ClaimTypes.NameIdentifier); });
    option.DefaultPolicy = option.GetPolicy(defaultPolicy)!;
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<AppDatabaseContext>();
// builder.Services.AddScoped<SmsService>();
builder.Services.AddScoped<SavingImageService>();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IPhoneAuthRepository, PhoneAuthRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IReservationTableRepository, ReservationTableRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IPhoneAuthService, PhoneAuthService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IReservationTableService, ReservationTableService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:5173");
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowCredentials();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image")),
    RequestPath = "/image"
});

app.MapControllers();

using var scope = app.Services.CreateScope();

using var context = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
//
// context.Database.EnsureDeleted();
// context.Database.EnsureCreated();

app.Run();