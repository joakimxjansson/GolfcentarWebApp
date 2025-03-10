using WebApplication4.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LoginConnectionString");


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<GolfContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddScoped<CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();