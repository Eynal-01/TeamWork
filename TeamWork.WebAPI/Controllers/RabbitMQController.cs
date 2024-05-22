using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Linq;

namespace TeamWork.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMQController : ControllerBase
    {
        private readonly string _queueName = "team-queue";
        private readonly ConnectionFactory _factory;

        public RabbitMQController()
        {
            _factory = new ConnectionFactory() { HostName = "amqps://bvpfaljc:C-0cjkCgLjHksx-723tnlsPbkKVvV6yE@roedeer.rmq.cloudamqp.com/bvpfaljc" };
        }

        // GET: api/<RabbitMQController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var messages = GetMessagesFromQueue();
            return messages;
        }

        // GET api/<RabbitMQController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RabbitMQController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            SendMessageToQueue(value);
        }

        // PUT api/<RabbitMQController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RabbitMQController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private IEnumerable<string> GetMessagesFromQueue()
        {
            var messages = new List<string>();

            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var result = channel.BasicGet(_queueName, true);

                while (result != null)
                {
                    var body = result.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    messages.Add(message);
                    result = channel.BasicGet(_queueName, true);
                }
            }

            return messages;
        }

        [HttpGet]
        private void SendMessageToQueue(string message)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: _queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}