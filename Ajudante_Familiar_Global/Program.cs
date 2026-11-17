using Ajudante_Familiar_Global.Application.Services;
using Ajudante_Familiar_Global.Application;
using Ajudante_Familiar_Global.Domain.Interfaces.IService;
using Ajudante_Familiar_Global.Infrastruture.Data;
using Ajudante_Familiar_Global.Infrastruture.Repository;
using HealthChecks.UI.Client;
using IdempotentAPI.Cache.DistributedCache.Extensions.DependencyInjection;
using IdempotentAPI.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using Serilog.Events;
using System.Text;
using System.Threading.RateLimiting;
using Ajudante_Familiar_Global.Application.Services.Token;
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} - {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    // Integrar Serilog com o host
    builder.Host.UseSerilog();

    //Parte de Banco de dados

    //oracle
    var connectionString = builder.Configuration.GetConnectionString("OracleConnection");

    builder.Services.AddDbContext<AppDbContext>(
       options =>
       options.UseOracle(connectionString));

    //Parte de HealthCheck

    builder.Services.AddHealthChecks()
    .AddCheck("self", () => Microsoft
    .Extensions
    .Diagnostics
    .HealthChecks.HealthCheckResult.Healthy(), tags: new[] { "live" })
    .AddDbContextCheck<AppDbContext>("oracle", tags: new[] { "ready" });

    //repositories e services
    builder.Services.AddApplicationServices();

    //Parte de Controllers
    builder.Services.AddControllers();
    // adicione aqui os outros repositories (Consulta, Medicamento, Atividade...)

    //services
    builder.Services.AddScoped<IAlimentacaoService, AlimentacaoService>();
    // adicione aqui os outros services

    //Parte de Controllers
    builder.Services.AddControllers();

    //Parte de Idempotência
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddIdempotentAPI();
    builder.Services.AddIdempotentAPIUsingDistributedCache();

    //Parte de Rate Limiting
    builder.Services.AddRateLimiter(options =>
    {
        options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

        // o nome "fixed" deve ser o mesmo usado nos [EnableRateLimiting("...")] dos controllers
        options.AddFixedWindowLimiter("fixed", opt =>
        {
            opt.PermitLimit = 10;
            opt.Window = TimeSpan.FromSeconds(10);
            opt.QueueLimit = 0;
        });
    });

    //Parte de JWT
    var jwtKey = builder.Configuration["Jwt:Key"];
    var jwtIssuer = builder.Configuration["Jwt:Issuer"];
    var jwtAudience = builder.Configuration["Jwt:Audience"];

    builder.Services.AddSingleton<TokenService>();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey!)),

            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,

            ValidateAudience = true,
            ValidAudience = jwtAudience,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

    builder.Services.AddAuthorization();

    //Parte de Swagger
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Ajudante Familiar API",
            Version = "v1",
            Description = "API para organização de cuidados com idosos"
        });

        var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Insira o token JWT para realizar a autenticação"
        });
        options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ajudante Familiar API");
            options.RoutePrefix = "swagger";
        });
    }

    app.UseHttpsRedirection();

    // Request logging estruturado com Serilog
    app.UseSerilogRequestLogging();

    app.UseRateLimiter();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapHealthChecks("/health/live", new HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("live")
    });

    app.MapHealthChecks("/health/ready", new HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("ready"),
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }