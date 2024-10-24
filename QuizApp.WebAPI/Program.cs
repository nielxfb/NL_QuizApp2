using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using QuizApp.Application.Commands.Handlers.Option;
using QuizApp.Application.Commands.Handlers.Question;
using QuizApp.Application.Commands.Handlers.Quiz;
using QuizApp.Application.Commands.Handlers.Response;
using QuizApp.Application.Commands.Handlers.Schedule;
using QuizApp.Application.Commands.Handlers.User;
using QuizApp.Application.Commands.Handlers.UserSchedule;
using QuizApp.Application.Commands.Handlers.UserScore;
using QuizApp.Application.Commands.Option;
using QuizApp.Application.Commands.Question;
using QuizApp.Application.Commands.Quiz;
using QuizApp.Application.Commands.Response;
using QuizApp.Application.Commands.Schedule;
using QuizApp.Application.Commands.User;
using QuizApp.Application.Commands.UserSchedule;
using QuizApp.Application.Commands.UserScore;
using QuizApp.Application.DTOs.Option;
using QuizApp.Application.DTOs.Question;
using QuizApp.Application.DTOs.Quiz;
using QuizApp.Application.DTOs.Response;
using QuizApp.Application.DTOs.Schedule;
using QuizApp.Application.DTOs.User;
using QuizApp.Application.DTOs.UserSchedule;
using QuizApp.Application.DTOs.UserScore;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Handlers.Option;
using QuizApp.Application.Queries.Handlers.Question;
using QuizApp.Application.Queries.Handlers.Quiz;
using QuizApp.Application.Queries.Handlers.Response;
using QuizApp.Application.Queries.Handlers.Schedule;
using QuizApp.Application.Queries.Handlers.User;
using QuizApp.Application.Queries.Handlers.UserSchedule;
using QuizApp.Application.Queries.Handlers.UserScore;
using QuizApp.Application.Queries.Option;
using QuizApp.Application.Queries.Question;
using QuizApp.Application.Queries.Quiz;
using QuizApp.Application.Queries.Response;
using QuizApp.Application.Queries.Schedule;
using QuizApp.Application.Queries.User;
using QuizApp.Application.Queries.UserSchedule;
using QuizApp.Application.Queries.UserScore;
using QuizApp.Application.Services;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;
using QuizApp.Infrastructure.Configurations;
using QuizApp.Infrastructure.Persistence;
using QuizApp.Infrastructure.Repositories;
using QuizApp.Infrastructure.Services;
using QuizApp.Infrastructure.SignalR;

var options = new WebApplicationOptions
{
    WebRootPath = "wwwroot",
    Args = args
};

var builder = WebApplication.CreateBuilder(options);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your JWT token"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddSingleton<JwtTokenService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var secret = configuration.GetValue<string>("Jwt:Secret");
    var issuer = configuration.GetValue<string>("Jwt:Issuer");
    var audience = configuration.GetValue<string>("Jwt:Audience");
    return new JwtTokenService(secret!, issuer!, audience!);
});

builder.Services.AddMemoryCache();

builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IUserScheduleRepository, UserScheduleRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IOptionRepository, OptionRepository>();
builder.Services.AddScoped<IResponseRepository, ResponseRepository>();
builder.Services.AddScoped<IUserScoreRepository, UserScoreRepository>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<ICommandHandler<RegisterUserCommand>, RegisterUserHandler>();
builder.Services.AddScoped<ICommandHandler<AddQuizCommand>, AddQuizHandler>();
builder.Services.AddScoped<ICommandHandler<RemoveQuizCommand>, RemoveQuizHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateQuizCommand>, UpdateQuizHandler>();
builder.Services.AddScoped<ICommandHandler<AddScheduleCommand>, AddScheduleHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateScheduleCommand>, UpdateScheduleHandler>();
builder.Services.AddScoped<ICommandHandler<RemoveScheduleCommand>, RemoveScheduleHandler>();
builder.Services.AddScoped<ICommandHandler<AddUserToScheduleCommand>, AddUserToScheduleHandler>();
builder.Services.AddScoped<ICommandHandler<RemoveUserFromScheduleCommand>, RemoveUserFromScheduleHandler>();
builder.Services.AddScoped<ICommandHandler<AddQuestionCommand>, AddQuestionHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateQuestionCommand>, UpdateQuestionHandler>();
builder.Services.AddScoped<ICommandHandler<RemoveQuestionCommand>, RemoveQuestionHandler>();
builder.Services.AddScoped<ICommandHandler<AddImageUrlCommand>, AddImageUrlHandler>();
builder.Services.AddScoped<ICommandHandler<AddOptionCommand>, AddOptionHandler>();
builder.Services.AddScoped<ICommandHandler<RemoveOptionCommand>, RemoveOptionHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateOptionCommand>, UpdateOptionHandler>();
builder.Services.AddScoped<ICommandHandler<AddResponseCommand>, AddResponseHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateResponseCommand>, UpdateResponseHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateStatusCommand>, UpdateStatusHandler>();
builder.Services.AddScoped<ICommandHandler<AddUserScoreCommand>, AddUserScoreHandler>();

builder.Services.AddScoped<IQueryHandler<LoginUserQuery, UserDetailsDto>, LoginUserHandler>();
builder.Services.AddScoped<IQueryHandler<LoginByCookieQuery, UserDetailsDto>, LoginByCookieHandler>();
builder.Services.AddScoped<IQueryHandler<GetUsersQuery, List<UserDetailsDto>>, GetUsersHandler>();
builder.Services.AddScoped<IQueryHandler<GetQuizzesQuery, List<QuizDetailsDto>>, GetQuizzesHandler>();
builder.Services.AddScoped<IQueryHandler<GetQuizByIdQuery, QuizDetailsDto>, GetQuizByIdHandler>();
builder.Services.AddScoped<IQueryHandler<GetSchedulesQuery, List<ScheduleDetailsDto>>, GetSchedulesHandler>();
builder.Services.AddScoped<IQueryHandler<GetScheduleByIdQuery, ScheduleDetailsDto>, GetScheduleByIdHandler>();
builder.Services.AddScoped<IQueryHandler<GetUsersInScheduleQuery, UsersInScheduleDto>, GetUsersInScheduleHandler>();
builder.Services.AddScoped<IQueryHandler<GetUserSchedulesQuery, List<UserSchedulesDto>>, GetUserSchedulesHandler>();
builder.Services.AddScoped<IQueryHandler<GetQuestionsInQuizQuery, List<QuestionDto>>, GetQuestionsInQuizHandler>();
builder.Services.AddScoped<IQueryHandler<GetByQuestionIdQuery, List<OptionDto>>, GetByQuestionIdHandler>();
builder.Services.AddScoped<IQueryHandler<GetUserResponsesInQuizQuery, ResponseDto>, GetUserResponsesInQuizHandler>();
builder.Services.AddScoped<IQueryHandler<GetUserScoreQuery, UserScoreDto>, GetUserScoreHandler>();
builder.Services.AddScoped<IQueryHandler<GetScoresByUserQuery, List<UserScoreDto>>, GetScoresByUserHandler>();

builder.Services.AddSignalR();
builder.Services.AddHostedService<TimeBroadcastService>();
builder.Services.AddHostedService<ResponsesConsumerService>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]!);
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

var app = builder.Build();

app.MapHub<TimeHub>("/TimeHub");

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Starting application..");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();