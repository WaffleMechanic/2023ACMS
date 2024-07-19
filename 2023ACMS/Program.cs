using _2023ACMS.Models;
using _2023ACMS.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Okta.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Get access to the application's configuration in the appsettings.json file.
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddTransient<IEmailService, EmailServiceMailKit>();
//builder.Services.AddScoped<IFileUploadService, LocalFileUploadService>(); //This is needed for file uploads.

builder.Services.AddRazorPages();

builder.Services.AddDbContext<_2023ACMSContext>(options => options.UseSqlServer(configuration["ConnectionStrings:2023ACMSConnectionString"]));

builder.Services.AddSession(options =>
{
    // Set the length of the session timeout.
    options.IdleTimeout = TimeSpan.FromMinutes(120);
});

// Okta: Set up authentication.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OktaDefaults.MvcAuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.AccessDeniedPath = PathString.FromUriComponent("/Home/DenyAccess");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    options.LoginPath = new PathString("/");
    options.LogoutPath = new PathString("/");
})
.AddOktaMvc(new OktaMvcOptions
{
    ClientId = configuration["Okta:ClientId"],
    ClientSecret = configuration["Okta:ClientSecret"],
    OktaDomain = configuration["Okta:OktaDomain"]
});

// Okta: This is required for Okta.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Make the specified subdirectory the path base of the application so that
// the application can run under the apps.franklincollege.edu subdomain.
// Do not start any redirects in the PageModel classes with forward slashes.
app.UsePathBase("/2023ACMS");
app.Use((context, next) =>
{
    context.Request.PathBase = "/2023ACMS";
    return next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication(); // Okta: Enable Okta authentication.

app.UseAuthorization(); // Okta: Enable Okta authorization.

// Okta: This is required for Okta.
#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Default}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
