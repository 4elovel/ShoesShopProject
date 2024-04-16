
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ShoesShopProject.Infrastructure;
using ShoesShopProject.Models;
using ShoesShopProject.Services;

namespace ShoesShopProject;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAutoMapper(typeof(AppMappingProfile));
        builder.Services.AddTransient<IEmailSender, EmailSender>();
        string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));
        builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationContext>()
        .AddApiEndpoints();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication()
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "767429345266-g87ppdg4s9mglpl98l6rl5hupc69p02v.apps.googleusercontent.com";
                googleOptions.ClientSecret = "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";
            })
            ;

        builder.Services.AddAuthorizationBuilder();

        // Add services to the container.
        builder.Services.AddControllersWithViews();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        //app.MapIdentityApi<IdentityUser>();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            // ...
            endpoints.MapRazorPages();
        });


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
