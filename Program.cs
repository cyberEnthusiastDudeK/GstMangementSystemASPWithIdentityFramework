using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectInfinityGST.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        /*services.AddMvc().AddRazorOptions(options =>
        {
            options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
            // Add other view location formats if needed
        });*/


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        // Map default route for Home Controller
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Map route for Admin Controller
        app.MapControllerRoute(
            name: "admin",
            pattern: "{controller=Admin}/{action=IndexAdmin}/{id?}");

        app.MapRazorPages();

        app.Run();
    }
}