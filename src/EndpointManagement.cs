namespace AddNServiceBus
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using NServiceBus;

    class EndpointManagement : IHostedService
    {
        private readonly SessionAndConfigurationHolder holder;
        private IEndpointInstance endpoint;

        public EndpointManagement(SessionAndConfigurationHolder holder)
        {
            this.holder = holder;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            endpoint = await Endpoint.Start(holder.Configuration)
                .ConfigureAwait(false);
            holder.Session = endpoint;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await endpoint.Stop()
                .ConfigureAwait(false);

            holder.Session = null;
        }
    }
}
