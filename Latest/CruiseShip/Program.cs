using CruiseShip.Data;
using CruiseShip.Data.Repository;
using CruiseShip.Data.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add identity services for authentication and authorization, requiring email confirmation for sign-in
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    // Use Entity Framework Core to store identity data in the ApplicationDbContext
    .AddEntityFrameworkStores<ApplicationDbContext>()
    // Add default token providers for account confirmation, password reset, etc.
    .AddDefaultTokenProviders();

// Add support for Razor Pages

// Add support for MVC pattern with controllers and views
builder.Services.AddControllersWithViews();

// Register the FacilityRepository class to be used whenever the IFacilityRepository interface is requested.
// This registration has a scoped lifetime, meaning a new instance of FacilityRepository will be created for each HTTP request.
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();   

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Voyager}/{controller=Home}/{action=Index}/{id?}");


// Create roles and default admin user
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roleNames = { "Admin", "Voyager" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Optionally, create a default admin user
    var adminUser = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com" };
    string adminPassword = "Admin@123";
    var user = await userManager.FindByEmailAsync(adminUser.Email);
    if (user == null)
    {
        var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
        if (createAdmin.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

app.Run();
