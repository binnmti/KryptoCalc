using Microsoft.EntityFrameworkCore;
using KryptoCalc.Server.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<KryptoCalcServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KryptoCalcServerContext") ?? throw new InvalidOperationException("Connection string 'KryptoCalcServerContext' not found.")));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();
