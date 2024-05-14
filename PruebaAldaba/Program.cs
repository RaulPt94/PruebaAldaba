using Microsoft.Extensions.Configuration;
using PruebaAldaba;
using PruebaAldaba.Brokers;
using PruebaAldaba.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Credential>(provider =>
{
    string token = provider.GetRequiredService<IConfiguration>().GetValue<string>("MovieConfiguration:token");
    return new Credential { Token = token };
});

builder.Services.AddMemoryCache();

builder.Services.AddTransient<IMovieBroker, MovieBroker>();
builder.Services.AddTransient<IMovieService, MovieService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
