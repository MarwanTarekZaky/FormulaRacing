using Microsoft.AspNetCore.Mvc.Razor;
using Domain.Models;
using Infrastructure.AutoMapper;
using Infrastructure.IRepository;
using Infrastructure.MainContext;
using Infrastructure.Repository;
using IoC.Services.Implementation;
using IoC.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
// using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
    // .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IRaceService, RaceService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile)); 


AddingMultiLanguageSupportServices(builder);

var app = builder.Build();

AddingMultiLanguageSupport(app);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapRazorPages();

app.Run();


 static void AddingMultiLanguageSupportServices(WebApplicationBuilder? builder)
{
    if (builder == null) { throw new Exception("builder==null"); };

    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
    builder.Services.AddMvc()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        var supportedCultures = new[] { "en", "ar"  };
        options.SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);
    });
}

 static void AddingMultiLanguageSupport(WebApplication? app)
{
    app?.UseRequestLocalization();
}