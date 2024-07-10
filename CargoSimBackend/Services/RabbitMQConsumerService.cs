namespace CargoSimBackend.Services;
using Polly;
using Polly.Retry;
using CargoSimBackend.DTO_s;
using CargoSimBackend.Services.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class RabbitMQConsumerService : BackgroundService
{
    private  IConnection _connection;
    private  IModel _channel;
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IOrderService _orderService;
    private readonly ISimulationService _simulationService;

    public RabbitMQConsumerService(IHubContext<NotificationHub> _hubContext,IOrderService orderService,ISimulationService simulationService)
    {
          var policy = Policy
                .Handle<Exception>()
                .WaitAndRetry(7, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        Console.WriteLine($"Retry {retryCount} encountered an error: {exception.Message}. Waiting {timeSpan} before next retry.");
                    });

            policy.Execute(() =>
            {
                var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: "HahnCargoSim_NewOrders", durable: false, exclusive: false, autoDelete: false, arguments: null);
            });
        this._hubContext = _hubContext;
        _orderService = orderService;
        _simulationService = simulationService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received +=async  (model, ea) =>
        {
            if(!this._simulationService.simulationIsRuning())
            {
                return;
            }
            var body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);
            Order order=JsonConvert.DeserializeObject<Order>(message);  
            var hasError=await this._orderService.AppendOrder(order);
            if(!string.IsNullOrWhiteSpace(hasError))
            {
                throw new Exception("Order Not Added");
            }
            await this._orderService.CechkOrderToAccept();
            var (orders,error)=await this._orderService.GetOrders();
            await _hubContext.Clients.All.SendAsync("ordersHub",   JsonConvert.SerializeObject(orders));
          //  Console.WriteLine(message);
            // Process the message here
        };
        _channel.BasicConsume(queue: "HahnCargoSim_NewOrders", autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}

