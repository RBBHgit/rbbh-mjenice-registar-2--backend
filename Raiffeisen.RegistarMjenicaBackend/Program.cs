using Raiffeisen.RegistarMjenicaBackend.Middleware;
using Raiffeisen.RegistarMjenicaBackend.Services.AppConfigs;
using Raiffeisen.RegistarMjenicaBackend.Services.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RBBHApiConfig>(builder.Configuration.GetSection("RBBHApi"));

builder.Services.AddMemoryCache();

builder.Services.Configure<RBBHApiConfig>(builder.Configuration.GetSection("RBBHApi"));
builder.Services.AddHttpClient("RBBHApiClient",
    c => { c.BaseAddress = new Uri(builder.Configuration.GetSection("RBBHApi:BaseURL").Value ?? string.Empty); });


builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = _ => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});


var section = builder.Configuration.GetSection("Oidc");
var oidcConfig = section.Get<OidcConfig>();


// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//         options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//     })
//     .AddCookie();

builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<CustomExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthentication();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();