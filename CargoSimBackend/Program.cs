using CargoSimBackend;
using CargoSimBackend.Services;
using CargoSimBackend.Services.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHostedService<RabbitMQConsumerService>();
builder.Services.AddSignalR();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)  .AddCookie(options =>
    {
        options.LoginPath = "/api/v1/user/login"; 
        options.AccessDeniedPath = "/api/v1/user/forbidden"; 
        options.Events = new CookieAuthenticationEvents
        {
             OnRedirectToLogin = context =>
            {
                // Intercept the redirect and modify the return URL
                if (!string.IsNullOrEmpty(context.Request.Path.Value))
                {
                    var returnUrl =context.Request.Path.Value;
                    context.Response.Redirect(returnUrl);
                }
                else
                {
                    context.Response.Redirect(options.LoginPath);
                }

                return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = 403;
                return Task.CompletedTask;
            }
        };
    });


/************ Register Services *************/
builder.Services.AddSingleton<IRestClient,RestClient>();
builder.Services.AddSingleton<IConfigurationService,ConfigurationService>();
builder.Services.AddSingleton<IAuthService,AuthService>();
builder.Services.AddSingleton<IOrderService,OrderService>();
builder.Services.AddSingleton<IGridService,GridService>();
builder.Services.AddSingleton<ISimulationService,SimulationService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") // React app's URL
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

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

app.UseAuthorization();
app.UseAuthentication();

app.UseCors("AllowReactApp");



app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub").RequireCors("AllowReactApp");
app.Run();
