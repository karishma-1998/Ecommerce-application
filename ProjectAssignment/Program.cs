using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ProjectAssignment.Data;
using ProjectAssignment.Middleware;
using ProjectAssignment.Models;
using ProjectAssignment.ProductRepo;

var builder = WebApplication.CreateBuilder(args);

//environment-specific appsettings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

//injecting DBContext
builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dbconn")));

// Add repositories.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Registering swagger generator
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Ecommerce Swagger API",
        Description = "Ecommerce ASP.Net Core Web API",
    });
});

var app = builder.Build();

//Register error handling middleware
app.UseMiddleware<Errorhandling>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.SerializeAsV2 = true;
    });
    //swagger Json  Endpoints
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
