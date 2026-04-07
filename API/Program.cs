using API.Middleware;
using Application.Categories.Commands;
using Application.Core;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



builder.Services.AddDbContext<AppDBContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Application.Categories.Commands.CreateCategoryCommand).Assembly));

// Auto Mapper 
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

// FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(CreateCategoryValidator).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



var app = builder.Build();

// Middleware JSON propre
app.UseMiddleware<ExceptionMiddleware>();


// Apply migrations + seed data (dev-friendly defaults)
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();

        await db.Database.MigrateAsync();
        await new DbInitializer().SeedData(db);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occured during migration.");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
