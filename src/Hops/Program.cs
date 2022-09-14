using Hops.Models;
using Hops.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BrewDBContext>();

builder.Services.AddScoped<IHopRepository, HopRepository>();
builder.Services.AddScoped<IMaltRepository, MaltRepository>();
builder.Services.AddScoped<IYeastRepository, YeastRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllers();

app.Run();