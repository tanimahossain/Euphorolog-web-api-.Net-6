global using Euphorolog.Database.Context;
using Euphorolog.GlobalExceptionHandler;
using Euphorolog.Repository.Contracts;
using Euphorolog.Repository.Repositories;
using Euphorolog.Services.Contracts;
using Euphorolog.Services.CustomExceptions;
using Euphorolog.Services.Services;
using Euphorolog.Services.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using FluentValidation;
using FluentValidation.AspNetCore;
using static System.Net.Mime.MediaTypeNames;
using Euphorolog.Services.DTOValidators.AuthDTOValidators;
using System.Reflection;
using Euphorolog.ContentNegotiation;
using Euphorolog.Services.DTOValidators;
using Euphorolog.Services.DTOs.AuthDTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;
builder.Services.AddDbContext<EuphorologContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("Euphorolog",
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:3001",
                                                  "https://localhost:3001")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
});

builder.Services.AddMvc(
            option =>
            {
                option.OutputFormatters.Add(new PlainTextCustomOutputFormatter());
                option.OutputFormatters.Add(new CSVCustomOutputFormatter());
                option.OutputFormatters.Add(new HTMLCustomOutputFormatter());
            }
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<MainDTOValidator<LogInRequestDTO>, LogInRequestDTOValidator>();
builder.Services.AddScoped<MainDTOValidator<SignUpRequestDTO>, SignUpRequestDTOValidator>();
builder.Services.AddScoped<IStoriesService, StoriesService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});
//builder.Services.AddScoped<IUtilities, Utilities>();
builder.Services.AddScoped<IStoriesRepository, StoriesRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:JWTSecretKey").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();
app.CustomErrorHandler();
app.UseHttpsRedirection();
app.UseCors("Euphorolog");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
