using System.Reflection;
using BalanceService.Balances.Application.Queries;
using BalanceService.Balances.Infrastructure.Cross_Cutting;
using BalanceService.Balances.Infrastructure.Repositories;
using BalanceService.Balances.Presentation.BackgroundServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();

builder.Services.AddDbContext<BalancesContext>(
    options => options.UseMySql(
        builder.Configuration.GetConnectionString("WebApiDatabase"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("WebApiDatabase")))
);

// builder.Services.AddHostedService<UpdateBalanceConsumerService>();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<BalancesContext>();
    context.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/balances/{accountId}", async (Guid accountId, IMediator mediator) =>
    {
        var query = new GetBalanceByAccountId.Query(accountId);
        var account = await mediator.Send(query);

        return Results.Ok(account);

    })
    .WithName("GetBalances")
    .WithOpenApi();

app.Run();