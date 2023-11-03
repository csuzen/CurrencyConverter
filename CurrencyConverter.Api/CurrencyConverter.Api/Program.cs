using CurrencyConverter.Core.Services.Impls;
using CurrencyConverter.Core.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICurrencyService, CurrencyService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("CorsApp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsApp");

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
    var response = new { error = exception?.Message ?? "Undefined Error" };
    await context.Response.WriteAsJsonAsync(response);
}));

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
