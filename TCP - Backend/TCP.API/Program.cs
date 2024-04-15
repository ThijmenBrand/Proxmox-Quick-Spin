using Carter;
using TCP.Application;
using TCP.ProxmoxInteractor;
using TCP.ProxmoxInteractor.Repositories;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("*");
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine(AppDomain.CurrentDomain);

builder.Services.AddCarter();
builder.Services.RegisterApplicationModule();
builder.Services.RegisterProxmoxModule();

builder.Services.AddSingleton<IProxmoxRepository, ProxmoxRepository>();

var app = builder.Build();

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.MapCarter();

app.Run();