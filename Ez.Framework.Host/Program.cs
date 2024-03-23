
using Carter;
using Ez.Application;
using Ez.Domain;
using Ez.Domain.Publishers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Migrations;
using IConsumer = Ez.Domain.Consumers.IConsumer;

SetThreadPool();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var file = Path.Combine(AppContext.BaseDirectory, "Ez.Framework.Host.xml");
    var path = Path.Combine(AppContext.BaseDirectory, file);
    c.IncludeXmlComments(path ,true);
});
builder.Services.AddApplication();
builder.Services.AddDomain();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddStackExchangeRedisCache(options =>
    options.Configuration = builder.Configuration.GetConnectionString("Cache"));
//配置MassTransit
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    //RabbitMq
    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]);
            h.Password(builder.Configuration["MessageBroker:Password"]);
        });
        configurator.ConfigureEndpoints(context);
    });
    //Memory
    busConfigurator.UsingInMemory((context,config)=>config.ConfigureEndpoints(context));
    
    //Sign in Consumer 
    var consumers = typeof(IConsumer).Assembly.GetTypes()
        .Where(type => type is { IsClass: true, IsAbstract: false } && typeof(IConsumer).IsAssignableFrom(type));
    foreach (var consumer in consumers)
    {
        busConfigurator.AddConsumer(consumer);
    }
});
//测试后台运行self-publisher and self-consumer
builder.Services.AddHostedService<MessagePublisher>();


//Carter
builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //自动迁移
    app.ApplyMigrations();
}
//Carter
app.MapCarter();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();


static void SetThreadPool()
{
    ThreadPool.GetMinThreads(out var minWorkerThreads, out var minCompletionPortThreads);
    ThreadPool.SetMinThreads(
        minWorkerThreads < 100 ? 100 : minWorkerThreads,
        minCompletionPortThreads < 100 ? 100 : minCompletionPortThreads);
    ThreadPool.GetMaxThreads(out var maxWorkerThreads, out var maxCompletionPortThreads);
    ThreadPool.SetMaxThreads(maxWorkerThreads, maxCompletionPortThreads <= 3000 ? 3000 : maxCompletionPortThreads);
}
