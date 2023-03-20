using Api.Middlewares;
using Business;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Contexts;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddBusiness();

_ = builder.Host.UseSerilog((_, lc) => lc
            .Enrich.FromLogContext()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.File(GetSerilogFileConfig(), rollingInterval: RollingInterval.Day));
_ = builder.Services.Configure((JsonOptions opt) =>
{
    opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    opt.SerializerOptions.PropertyNameCaseInsensitive = true;
    opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

_ = app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.Run();

static string GetSerilogFileConfig()
{
    var logPath = AppDomain.CurrentDomain.BaseDirectory;
    return @$"{logPath}\logs\log-.txt";
}

try
{
    Log.Information("Starting host");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}


public partial class Program { }