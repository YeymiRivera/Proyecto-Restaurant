using proyecto.Components;
using Microsoft.EntityFrameworkCore;
 using proyecto.Models; 
 using proyecto.Services; 
 using Microsoft.AspNetCore.Components.Authorization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<MesaService>();
// Registrar el DbContext
builder.Services.AddDbContextFactory<RhdbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 1. Sistema central de autorización 
builder.Services.AddAuthorizationCore(); 
builder.Services.AddAuthorization();

builder.Services.AddAuthentication();
// 2. Proveedor personalizado 
builder.Services.AddScoped<CustomAuthStateProvider>(); 

// 3. Mapeo al proveedor nativo
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>()); 
// 4. Estado en cascada 
builder.Services.AddCascadingAuthenticationState(); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();
