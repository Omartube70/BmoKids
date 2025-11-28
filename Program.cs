using BmoKids.Components;
using BmoKids.Services;
using BmoKids.Services.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register LocalStorage services
builder.Services.AddScoped<IStorageService, LocalStorageService>();
builder.Services.AddScoped<DataStorageService>();
builder.Services.AddScoped<SettingsStorageService>();
builder.Services.AddScoped<CacheStorageService>();

// Register BmoKids services
builder.Services.AddScoped<SpeechService>();
builder.Services.AddScoped<AssessmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();