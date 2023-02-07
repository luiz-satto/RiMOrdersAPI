using Application.Behaviors;
using Domain.Abstractions;
using FluentValidation;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Data;
using WebHost.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var presentationAssembly = Presentation.PresentationAssembly.Instance;
builder.Services.AddControllers()
    .AddApplicationPart(presentationAssembly);

var applicationAssembly = Application.ApplicationAssembly.Instance;
builder.Services.AddMediatR(applicationAssembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(applicationAssembly);
builder.Services.AddOptions();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // var presentationDocumentationFile = $"{presentationAssembly.GetName().Name}.xml";
    // var presentationDocumentationFilePath = Path.Combine("..\\Presentation", presentationDocumentationFile);
    // c.IncludeXmlComments(presentationDocumentationFilePath);
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var server = builder.Configuration["DBServer"] ?? "rimordersdb";
    var port = builder.Configuration["DBPort"] ?? "1433";
    var database = builder.Configuration["Database"] ?? "master";
    var user = builder.Configuration["DBUser"] ?? "SA";
    var password = builder.Configuration["DBPassword"] ?? "PaSSw0rd2023";
    options.UseSqlServer(@$"
        Server={server},{port};
        Initial Catalog={database};
        User ID={user};
        Password={password};
        Trust Server Certificate=True
    ");
});

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IUnitOfWork>(
    factory => factory.GetRequiredService<AppDbContext>());

builder.Services.AddScoped<IDbConnection>(
    factory => factory.GetRequiredService<AppDbContext>().Database.GetDbConnection());

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();
// Execute migrations
await ApplyMigrations(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();

static async Task ApplyMigrations(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}
