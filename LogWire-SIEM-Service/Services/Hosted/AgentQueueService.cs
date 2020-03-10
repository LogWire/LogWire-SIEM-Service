using System;
using System.Threading;
using System.Threading.Tasks;
using LogWire.SIEM.Service.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LogWire.SIEM.Service.Services.Hosted
{
    public class AgentQueueService : IHostedService
    {

        private IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;

        public AgentQueueService(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _configuration = configuration;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            RabbitManager.Instance.Startup(_configuration, _scopeFactory);
            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
