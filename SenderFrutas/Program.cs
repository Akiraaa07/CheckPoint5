using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "frutas",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var frutas = new[] { "Banana", "Maçã", "Uva", "Morango", "Abacaxi" };

foreach (var fruta in frutas)
{
    var body = Encoding.UTF8.GetBytes(fruta);

    channel.BasicPublish(exchange: "",
                         routingKey: "frutas",
                         basicProperties: null,
                         body: body);

    Console.WriteLine($"🍓 Fruta enviada: {fruta}");
}