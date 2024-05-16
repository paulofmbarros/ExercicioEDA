using BalanceService.Balances.Application.Commands.UpdateBalance;
using BalanceService.Balances.Application.DTOs;

namespace BalanceService.Balances.Presentation.BackgroundServices;

using Confluent.Kafka;
using MediatR;
using Newtonsoft.Json;

public class UpdateBalanceConsumerService : BackgroundService
{
    private readonly ILogger<UpdateBalanceConsumerService> logger;

    private readonly IConsumer<string,string> consumer;

    private readonly IServiceScopeFactory scopeFactory;

    public UpdateBalanceConsumerService(IConfiguration configuration, ILogger<UpdateBalanceConsumerService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        this.logger = logger;
        this.scopeFactory = serviceScopeFactory;

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = "InventoryConsumerGroup",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        this.consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        consumer.Subscribe("balances");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
             {
            this.logger.LogInformation("Consuming message from Kafka");
            var consumeResult = consumer.Consume(stoppingToken);

            // Process the consumed message
            this.logger.LogInformation($"Received message: {consumeResult.Message.Value}");

            var messageContent = JsonConvert.DeserializeObject<BalanceUpdatedEventDto>(consumeResult.Message.Value);

            var command = new UpdateBalanceCommand
            {

                    AccountIdFrom = messageContent.Payload.account_id_from,
                    AccountIdTo = messageContent.Payload.account_id_to,
                    BalanceAccountIdFrom = messageContent.Payload.balance_account_id_from,
                    BalanceAccountIdTo = messageContent.Payload.balance_account_id_to


            };

            using var scope = this.scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var result = await mediator.Send(command);

            this.logger.LogInformation("Message consumed with success");
            }
            catch (OperationCanceledException)
            {
                // Graceful shutdown
                break;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error consuming message from Kafka");
            }
        }
    }
}