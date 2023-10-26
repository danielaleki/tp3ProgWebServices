using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<WebApplication1Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApplication1Context") ??
                        throw new InvalidOperationException("Connection string 'WebApplication1Context' not found."));
    options.UseLazyLoadingProxies();
});
    

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<TripUser, IdentityRole>()
    .AddEntityFrameworkStores<WebApplication1Context>();

builder.Services.AddScoped<VoyageService>();

builder.Services.AddCors(options =>
{
        options.AddPolicy("Permettre tout", policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
        
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Permettre tout");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
