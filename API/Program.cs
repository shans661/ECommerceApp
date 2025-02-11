using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("API"));
});
 builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });

     // Automatically navigate to Swagger UI
    app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/swagger");
            return;
        }
        await next();
    });
}

app.MapControllers();
app.Run();

