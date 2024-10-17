using Eutonies.UI.Components;
using Eutonies.UI.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<IWordsLoader, WordsLoader>();

var app = builder.Build();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapBlazorHub()
   .WithOrder(-1);

app.UseRouting();
app.UseAntiforgery();

app.Run();
