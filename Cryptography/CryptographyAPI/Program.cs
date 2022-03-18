using CryptographyAPI.Services;
using CryptographyAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IAlgorithmService<PlayfairData>, PlayfairService>();
builder.Services.AddTransient<IAlgorithmService<PlayfairExtendedData>, PlayfairExtendedService>();

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
