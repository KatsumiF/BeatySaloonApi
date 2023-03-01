using BeatySaloonApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>c.EnableAnnotations());//отвечает за автоматическую генерацию документации, подключены аннотации)

builder.Services.AddDbContext<BeautySaloonBaseContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("BeautySaloonBase")));
builder.Services.AddMvcCore();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();//отвечает за автоматическую генерацию документации
    app.UseSwaggerUI();//отвечает за автоматическую генерацию документации
}

app.UseAuthorization();

app.MapControllers();

app.Run();
