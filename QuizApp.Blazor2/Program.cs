using Blazored.SessionStorage;
using QuizApp.Blazor2.Components;
using QuizApp.Blazor2.Services;
using QuizApp.Blazor2.Utils;
using QuizApp.Domain.Interfaces;
using QuizApp.Infrastructure.Configurations;
using QuizApp.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5160/") });
builder.Services.AddScoped<ICookie, Cookie>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<QuizService>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<OptionService>();
builder.Services.AddScoped<ScheduleService>();
builder.Services.AddScoped<UserScheduleService>();

builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddScoped(typeof(IRabbitMqPublisher<>), typeof(RabbitMqPublisher<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();