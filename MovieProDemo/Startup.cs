using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieProDemo.Data;
using MovieProDemo.Models.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieProDemo.Services;

namespace MovieProDemo
{
    public class Startup {

    public IConfiguration Configuration {
        get;
    }
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services) {

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(ConnectionService.GetConnectionString(Configuration)));

        services.AddRazorPages();


            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }
    public void Configure(WebApplication app, IWebHostEnvironment env) {
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();
        app.Run();
    }
}
}
