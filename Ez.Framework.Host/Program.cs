
using Carter;
using Ez.Application;
using Ez.Domain;
using Ez.Domain.DistributeEventsHandle.Publishers;
using Ez.Infrastructure;
using MassTransit;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Persistence;
using Persistence.ConstStrings;
using Persistence.Migrations;
using StackExchange.Redis;
using IConsumer = Ez.Domain.DistributeEventsHandle.Consumers.IConsumer;

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
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddDomain();
builder.Services.AddApplication();
// 使用 StackExchange.Redis 连接哨兵模式
// builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
// {
//     var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Cache1") ?? string.Empty);
//     configuration.CommandMap = CommandMap.Sentinel; // 使用哨兵模式
//     configuration.TieBreaker = ""; // 关闭 TieBreaker简化配置以及减少复杂度 集群规模较大开启可以避免脑裂问题（split-brain problem）。
//     configuration.DefaultVersion = new Version(6, 2, 1); // 设置适当的 Redis 版本
//     return ConnectionMultiplexer.Connect(configuration);
// });
// builder.Services.AddStackExchangeRedisCache(options =>
// {
//     options.Configuration = builder.Configuration.GetConnectionString("Cache1");
//     options.InstanceName = "SampleInstance"; // 此处的 InstanceName 用于标识不同的缓存实例
// });
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
//OpenTelemetry
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(DiagnosticsConfig.ServiceName))
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();
        metrics.AddMeter(DiagnosticsConfig.Meter.Name);
        metrics.AddOtlpExporter();
    })
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation();
        tracing.AddOtlpExporter();
    });

builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());
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
