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

// Uygulamada herhangi bir hata alýndýðýnda kullanýcýyý hata sayfasýna yönlendiriyor.Development-Production-Staging ortamlarýndan baðýmsýz olarak test amaçlý her türlü Exception Handler' ý çalýþtýrýyoruz.
// Sistem eðer hata alýrsa, o an ki hata alýnan sayfa url'i sabit kalýyor fakat arka planda Home/Error sayfasýndaki hata gösteriliyor.
// Bunun amacý; kullanýcý hata aldýðý sayfada refresh(F5) yaptýðýnda, eðer hata anlýksa veya düzelmiþse Home/Error sayfasýnda deðil ilgili sayfada kalmasý uygun olacaðý için bu yapýlýyor.
app.UseExceptionHandler("/Home/Error");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
