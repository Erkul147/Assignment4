using DataLayer;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDataService, DataService>();

builder.Services.AddMapster();

// Add services to the container.
builder.Services.AddMvcCore();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapSwagger().RequireAuthorization();
// app.UseRouting();

app.MapControllers();
app.Run();
