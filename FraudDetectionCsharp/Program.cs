using FraudDetectionCsharp.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FraudDetectionCsharp.Domain.services;
using FraudDetectionCsharp.Core.rules;
using FraudDetectionCsharp.Infra.database;
using Confluent.Kafka;
using FraudDetectionCsharp.Infra.messaging;
using Microsoft.Extensions.Options;
using FraudDetectionCsharp.Infra.repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurar KafkaSettings primeiro
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));

// Registrar o KafkaConfig para injeção
builder.Services.AddSingleton<KafkaConfig>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<KafkaSettings>>().Value;
    return new KafkaConfig { ConnectionString = settings.BootstrapServers };
});

// Registrar ProducerConfig
builder.Services.AddSingleton<ProducerConfig>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<KafkaSettings>>().Value;
    return new ProducerConfig { BootstrapServers = settings.BootstrapServers };
});

// Registrar o Producer
builder.Services.AddSingleton<Producer>(sp =>
{
    var kafkaConfig = sp.GetRequiredService<KafkaConfig>();
    var settings = sp.GetRequiredService<IOptions<KafkaSettings>>().Value;
    return new Producer(kafkaConfig, settings);
});



// Configuração do banco de dado
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro dos repositórios
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<PaymentService>();


builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<TransactionService>();

builder.Services.AddTransient<FraudReportRepository>();
builder.Services.AddTransient<FraudReportService>();

builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<NotificationRepository>();

builder.Services.AddScoped<FraudDetectionRules>();
builder.Services.AddScoped<PaymentFraudDetectionRules>();
builder.Services.AddScoped<TransactionFraudDetectionRules>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"))
        };
    });


builder.Services.AddControllers();
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

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
