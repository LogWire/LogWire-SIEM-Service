using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogWire.SIEM.Service.Data.Model;
using LogWire.SIEM.Service.Data.Repository;
using LogWire.SIEM.Service.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LogWire.SIEM.Service.Utils
{
    public class RabbitManager
    {

        private static RabbitManager _instance;
        private IServiceScopeFactory _scopeFactory;

        public static RabbitManager Instance => _instance ?? new RabbitManager();

        private string _exchangeName = "logwire.agent";
        private string _hostname = "localhost";
        private string _username = "guest";
        private string _password = "guest";

        private IModel agentUserModel;

        private RabbitManager()
        {

            using (var conn = GetConnection())
            {

                IModel model = conn.CreateModel();

                model.ExchangeDeclare(_exchangeName, ExchangeType.Topic);

                model.Close();
                conn.Close();

            }


        }

        private IConnection GetConnection()
        {

            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = _hostname,
                Password = _password,
                UserName = _username
            };

            return factory.CreateConnection();

        }

        public void Startup(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {

            _hostname = configuration["rabbitmq.guest.endpoint"];
            _password = configuration["rabbitmq.guest.pass"];

            _scopeFactory = scopeFactory;

            BindQueues();

        }

        private void BindQueues()
        {

            BindAgentEventsQueue();

        }

        private void BindAgentEventsQueue()
        {

            var conn = GetConnection();
            agentUserModel = conn.CreateModel();

            agentUserModel.QueueDeclare("logwire.siem.agent.user", false, false, false, null);
            agentUserModel.QueueBind("logwire.siem.agent.user", "logwire.agent", "event.user.*", null);

            var consumer = new EventingBasicConsumer(agentUserModel);

            consumer.Received += ConsumerOnReceived;
            agentUserModel.BasicConsume("logwire.siem.agent.user", false, consumer);

        }

        private void ConsumerOnReceived(object sender, BasicDeliverEventArgs e)
        {

            using var scope = _scopeFactory.CreateScope();
            var userRepo = scope.ServiceProvider.GetRequiredService<IDataRepository<UserEntry>>();
            var machineRepo = scope.ServiceProvider.GetRequiredService<IDataRepository<MachineEntry>>();
            var authEventRepo = scope.ServiceProvider.GetRequiredService<IDataRepository<AuthEventEntry>>() as AuthEventRepository;

            var message = JsonConvert.DeserializeObject<AgentAuthMessage>(Encoding.UTF8.GetString(e.Body));
            var user = userRepo.Get(message.Username);
            var machine = machineRepo.Get(message.MachineName);

            if (user == null)
            {
                user = new UserEntry
                {
                    Username = message.Username,
                    Id = Guid.NewGuid()
                };

                userRepo.Add(user);
            }

            if (machine == null)
            {
                machine = new MachineEntry
                {
                    Name = message.MachineName,
                    Ip = message.MachineIp,
                    Id = Guid.NewGuid()
                };

                machineRepo.Add(machine);
            }

            var eventDate = DateTime.Parse(message.EventDate);

            if (!authEventRepo.HasEvent(eventDate, machine.Id, user.Id, message.EventType))
            {
                authEventRepo.Add(new AuthEventEntry
                {
                    EventType = message.EventType,
                    EventTime = eventDate,
                    UserId = user.Id,
                    MachineId = machine.Id,
                    Id = Guid.NewGuid()
                });
            }

            agentUserModel.BasicAck(e.DeliveryTag, false);


        }
    }
}
