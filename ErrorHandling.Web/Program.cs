
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    // Uygulamada herhangi bir hata al�nd���nda kullan�c�y� hata sayfas�na y�nlendiriyor.Development-Production-Staging ortamlar�ndan ba��ms�z olarak test ama�l� her t�rl� Exception Handler' � �al��t�r�yoruz.
    // Sistem e�er hata al�rsa, o an ki hata al�nan sayfa url'i sabit kal�yor fakat arka planda Home/Error sayfas�ndaki hata g�steriliyor.
    // Bunun amac�; kullan�c� hata ald��� sayfada refresh(F5) yapt���nda, e�er hata anl�ksa veya d�zelmi�se Home/Error sayfas�nda de�il ilgili sayfada kalmas� uygun olaca�� i�in bu yap�l�yor.
    //app.UseExceptionHandler("/Home/Error");
    app.UseExceptionHandler(context =>
    {
        // ��erisinde sayfa g�sterece�imiz i�in Run metotunu �al��t�r�yoruz.
        context.Run(async page =>
        {
            // Server tarafl� bir hata oldu�u i�in 500 ile ba�lat�yoruz. Client hatas� olsayd� 400 ile ba�latacakt�k.Ba�ar�l� oldu�u durumlarda 200, y�nlendirme oldu�u durumlarda 300 kodlar� ile ba�lar. 
            page.Response.StatusCode = 500;
            page.Response.ContentType = "text/html";
            await page.Response.WriteAsync($"<html><head></head><h1>An error occured! {page.Response.StatusCode}</h1></html>");
        });
    });


    // 1.Y�ntem --------------
    // Middleware'e StatusCodePages metotunu ekliyoruz.Eklemez isek; kullan�c� olmayan bir url a�maya �al��t���nda anlams�z bir hata d�ner. Ekledi�imizde sistemi patlatmadan duruma uygun hata mesaj� ve hata kodu d�ner.
    //app.UseStatusCodePages("text/plain", "An error occured. Status Code:{0}");


    // 2.Y�ntem --------------
    // IApplicationBuilder varsa ctor i�erisinde uygulama i�erisinde customize kod yaz�labilir.(Herhangi bir metotun i�inde var ise IApplicationBuilder)
    app.UseStatusCodePages(async context =>
    {
        context.HttpContext.Response.ContentType = "text/plain";
        await context.HttpContext.Response.WriteAsync($"An error occured! Status Code: {context.HttpContext.Response.StatusCode}");
    });


    // 3.Y�ntem --------------
    //app.UseStatusCodePages();

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
