using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace RabbitMQ.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueController : ControllerBase
    {
        private const string RabbitMQHost = "localhost"; // Endereço do servidor RabbitMQ
        private const string QueueName = "minha_fila";   // Nome da fila

        [HttpPost("enviarmensagem")]
        public IActionResult EnviarMensagem([FromBody] string mensagem)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = RabbitMQHost,
                    Port = 5672,
                    UserName = "guest",
                    Password = "guest"
                };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(mensagem);

                    channel.BasicPublish(exchange: "",
                                         routingKey: QueueName,
                                         basicProperties: null,
                                         body: body);

                    return Ok($"Mensagem enviada: {mensagem}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
