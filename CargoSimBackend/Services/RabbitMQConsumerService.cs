namespace CargoSimBackend;

using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class RabbitMQConsumerService : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IHubContext<NotificationHub> _hubContext;
    private string message="";
    public  string getMessage(){
        return message;
    }
    public RabbitMQConsumerService(IHubContext<NotificationHub> _hubContext)
    {
        var factory = new ConnectionFactory() { HostName = "localhost", Port=5672 };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "HahnCargoSim_NewOrders", durable: false, exclusive: false, autoDelete: false, arguments: null);
        this._hubContext = _hubContext;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received +=async  (model, ea) =>
        {
            var body = ea.Body.ToArray();
             this.message = Encoding.UTF8.GetString(body);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message );
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

