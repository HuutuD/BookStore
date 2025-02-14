using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Configuration;
using BookStoreWeb.Models;
using BookStoreWeb.Services;
using BookStoreWeb;
using Microsoft.EntityFrameworkCore;
using BookShopBusiness;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpClient();
        

        // Register the ApiService
        builder.Services.AddScoped(typeof(ApiService<>));


        builder.Services.AddDbContext<BookDbContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("BookStore")));

        builder.Services.Configure<ApiUrls>(builder.Configuration.GetSection("ApiUrls"));
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseSession();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}