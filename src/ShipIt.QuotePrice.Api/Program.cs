using System.Text.Json.Serialization;
using ShipIt.PriceQuotes.Domain;
using ShipIt.PriceQuotes.Storage;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});

builder.Services.AddScoped<IPriceQuoteService, PriceQuoteService>();
builder.Services.AddScoped<IPriceQuoteRepository, PriceQuoteRepository>();

builder.Services.Configure<PriceQuoteRepositoryOptions>(
    builder.Configuration.GetSection(
        nameof(PriceQuoteRepositoryOptions)
    )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseExceptionHandler("/error-development");
else
    app.UseExceptionHandler("/error");

app.MapControllers();



app.Run();

