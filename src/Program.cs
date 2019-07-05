using System;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Threading.Tasks;

namespace msgprocessor
{
    class Program
    {        
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            Console.WriteLine("Registering EventProcessor...");

            Console.WriteLine(Environment.GetEnvironmentVariable("EventHubName") + " " + 
                        PartitionReceiver.DefaultConsumerGroupName + " " +
                        Environment.GetEnvironmentVariable("EventHubConnectionString") + " " +
                        Environment.GetEnvironmentVariable("StorageConnectionString") + " " +
                        Environment.GetEnvironmentVariable("StorageContainerName"));

            var eventProcessorHost = new EventProcessorHost(
                        Environment.GetEnvironmentVariable("EventHubName"),
                        PartitionReceiver.DefaultConsumerGroupName,
                        Environment.GetEnvironmentVariable("EventHubConnectionString"),
                        Environment.GetEnvironmentVariable("StorageConnectionString"),
                        Environment.GetEnvironmentVariable("StorageContainerName"));

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            while (true)
            {
                await Task.Delay(30000);
            }
        }
    }
}
