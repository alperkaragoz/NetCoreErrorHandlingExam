
using ErrorHandling.Web.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // Bu filtreyi ekledi�imde b�t�n controller s�n�flar� i�erisinde bu filter kullan�lacak.Her bir controller'�n �zerinde bu filter'� tan�tmam�za gerek kalm�yor b�ylece.
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
    // IApplicationBuilder varsa ctor i�erisinde uygulama i�erisinde customize kod yaz�labilir.(Herhangi bir metotun i�inde var ise IApplicationBuilder)
    app.UseStatusCodePages(async context =>
    {
        context.HttpContext.Response.ContentType = "text/plain";
        await context.HttpContext.Response.WriteAsync($"An error occured! Status Code: {context.HttpContext.Response.StatusCode}");
    });

    // Middleware'in g�z�kebilmesi i�in Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore'u k�t�phaneye eklememiz gerekiyor.
    // Entity Framework db hata sayfalar�.
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
