using System.Net;
using FarmBack.Services;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var repository = new SensorDataRepository("mongodb://mongodb:27017", "windfarm", "windfarm");

builder.Services.AddSingleton<ISensorDataRepository>(provider => repository);
builder.Services.AddControllers();
builder.Services.AddHostedService<RabbitMQConsumerService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://frontend:4200").AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.Configure<MvcOptions>(options =>
{
    options.Filters.Add(new RequireHttpsAttribute { Permanent = true });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
