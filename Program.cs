using Microsoft.EntityFrameworkCore;
using SistemaEscolar.Data;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// DbContext
builder.Services.AddDbContext<SistemaEscolarContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SistemaEscolarContext")
    )
);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
