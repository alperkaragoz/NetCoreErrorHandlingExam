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

// Uygulamada herhangi bir hata al�nd���nda kullan�c�y� hata sayfas�na y�nlendiriyor.Development-Production-Staging ortamlar�ndan ba��ms�z olarak test ama�l� her t�rl� Exception Handler' � �al��t�r�yoruz.
// Sistem e�er hata al�rsa, o an ki hata al�nan sayfa url'i sabit kal�yor fakat arka planda Home/Error sayfas�ndaki hata g�steriliyor.
// Bunun amac�; kullan�c� hata ald��� sayfada refresh(F5) yapt���nda, e�er hata anl�ksa veya d�zelmi�se Home/Error sayfas�nda de�il ilgili sayfada kalmas� uygun olaca�� i�in bu yap�l�yor.
app.UseExceptionHandler("/Home/Error");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
