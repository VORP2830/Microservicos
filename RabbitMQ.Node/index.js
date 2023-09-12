const amqp = require('amqplib');

async function consumeQueue() {
  const rabbitmqHost = 'localhost'; // Endereço do servidor RabbitMQ
  const queueName = 'minha_fila'; // Nome da fila

  try {
    const connection = await amqp.connect(`amqp://${rabbitmqHost}`);
    const channel = await connection.createChannel();

    await channel.assertQueue(queueName, { durable: false });
    console.log(`Aguardando mensagens na fila ${queueName}. Para sair, pressione Ctrl+C`);

    channel.consume(queueName, (message) => {
      if (message !== null) {
        const content = message.content.toString();
        console.log(`Mensagem recebida: ${content}`);
        channel.ack(message); // Confirma o recebimento da mensagem
      }
    });
  } catch (error) {
    console.error(`Erro ao conectar e consumir a fila: ${error.message}`);
  }
}

consumeQueue();
