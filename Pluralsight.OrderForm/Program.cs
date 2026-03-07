var builder = WebApplication.CreateBuilder(args);

// Lab environment specific: this stops .NET from automatically blocking iframes.
builder.Services.AddAntiforgery(options =>
{
    options.SuppressXFrameOptionsHeader = true;
});

builder.Services.AddRazorPages();

var app = builder.Build();

// Lab environment specific: set the security headers.
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Content-Security-Policy", "frame-ancestors 'self' https://app.pluralsight.com");

    // Remove the restrictive legacy header if something else injected it
    context.Response.Headers.Remove("X-Frame-Options");

    await next();
});

app.UseRouting();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
