using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "usuarios",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var usuarios = new[]
{
    "Giovana - gi@teste.com",
    "Carlos - carlos@teste.com",
    "Renata - re@teste.com"
};

foreach (var usuario in usuarios)
{
    var body = Encoding.UTF8.GetBytes(usuario);

    channel.BasicPublish(exchange: "",
                         routingKey: "usuarios",
                         basicProperties: null,
                         body: body);

    Console.WriteLine($"👤 Usuário enviado: {usuario}");
}
