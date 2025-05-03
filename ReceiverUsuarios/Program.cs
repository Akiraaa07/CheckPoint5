using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Consumindo a fila com usuários validados
channel.QueueDeclare(queue: "usuarios_validados",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var mensagem = Encoding.UTF8.GetString(body);
    Console.WriteLine($"📥 Usuário validado recebido: {mensagem}");
};

channel.BasicConsume(queue: "usuarios_validados",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine("✅ Aguardando usuários validados... Pressione [Enter] para sair.");
Console.ReadLine();