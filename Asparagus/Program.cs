using Asparagus.Persistance;
using Asparagus.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddScoped<FeedService, FeedService>();

// TODO: change for production
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});




var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}
using (var scope = builder.Services.BuildServiceProvider().CreateScope()) // invoke method of Db initialization
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<ApplicationDBcontext>(); // for accessing dependencies
    DBInitialazer.Initialize(context); // initialize database
}

app.UseStaticFiles();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Feed}/{action=Index}/{id?}");
app.Run();
