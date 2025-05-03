using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Consumindo a fila com frutas validadas
channel.QueueDeclare(queue: "frutas_validadas",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var mensagem = Encoding.UTF8.GetString(body);
    Console.WriteLine($"🍍 Fruta validada recebida: {mensagem}");
};

channel.BasicConsume(queue: "frutas_validadas",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine("✅ Aguardando frutas validadas... Pressione [Enter] para sair.");
Console.ReadLine();
