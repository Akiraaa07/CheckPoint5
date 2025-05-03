using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Fila de origem dos dados não validados
channel.QueueDeclare(queue: "frutas", durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.QueueDeclare(queue: "usuarios", durable: false, exclusive: false, autoDelete: false, arguments: null);

// Filas de destino com dados validados
channel.QueueDeclare(queue: "frutas_validadas", durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.QueueDeclare(queue: "usuarios_validados", durable: false, exclusive: false, autoDelete: false, arguments: null);

void ValidarEPublicar(string tipo, string mensagem)
{
    var destino = tipo == "fruta" ? "frutas_validadas" : "usuarios_validados";

    // Validação básica (exemplo)
    var valido = !string.IsNullOrWhiteSpace(mensagem) && mensagem.Length >= 3;

    if (valido)
    {
        var body = Encoding.UTF8.GetBytes(mensagem);
        channel.BasicPublish(exchange: "", routingKey: destino, basicProperties: null, body: body);
        Console.WriteLine($"✔️ {tipo} validado e enviado: {mensagem}");
    }
    else
    {
        Console.WriteLine($"❌ {tipo} inválido ignorado: {mensagem}");
    }
}

// Consumidor de frutas
var consumerFrutas = new EventingBasicConsumer(channel);
consumerFrutas.Received += (model, ea) =>
{
    var mensagem = Encoding.UTF8.GetString(ea.Body.ToArray());
    ValidarEPublicar("fruta", mensagem);
};
channel.BasicConsume(queue: "frutas", autoAck: true, consumer: consumerFrutas);

// Consumidor de usuários
var consumerUsuarios = new EventingBasicConsumer(channel);
consumerUsuarios.Received += (model, ea) =>
{
    var mensagem = Encoding.UTF8.GetString(ea.Body.ToArray());
    ValidarEPublicar("usuario", mensagem);
};
channel.BasicConsume(queue: "usuarios", autoAck: true, consumer: consumerUsuarios);

Console.WriteLine("🔎 Validation aguardando mensagens... Pressione [Enter] para sair.");
Console.ReadLine();
