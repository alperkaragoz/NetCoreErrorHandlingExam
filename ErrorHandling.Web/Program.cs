
using ErrorHandling.Web.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // Bu filtreyi eklediðimde bütün controller sýnýflarý içerisinde bu filter kullanýlacak.Her bir controller'ýn üzerinde bu filter'ý tanýtmamýza gerek kalmýyor böylece.
    options.Filters.Add(new CustomExceptionFilterAttribute() { ErrorPage="CustomError"});
});

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // IApplicationBuilder varsa ctor içerisinde uygulama içerisinde customize kod yazýlabilir.(Herhangi bir metotun içinde var ise IApplicationBuilder)
    app.UseStatusCodePages(async context =>
    {
        context.HttpContext.Response.ContentType = "text/plain";
        await context.HttpContext.Response.WriteAsync($"An error occured! Status Code: {context.HttpContext.Response.StatusCode}");
    });

    // Middleware'in gözükebilmesi için Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore'u kütüphaneye eklememiz gerekiyor.
    // Entity Framework db hata sayfalarý.
    app.UseDatabaseErrorPage();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
