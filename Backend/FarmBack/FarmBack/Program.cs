using FarmBack.Services;

var builder = WebApplication.CreateBuilder(args);

var repository = new SensorDataRepository("mongodb://localhost:27017", "windfarm", "windfarm");

builder.Services.AddSingleton<ISensorDataRepository>(provider => repository);
builder.Services.AddControllers();
builder.Services.AddHostedService<RabbitMQConsumerService>();
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
