using Goder.TestRunner.Configuartions;
using Goder.TestRunner.Models;
using Goder.TestRunner.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Wrapper.Models;
using RabbitMQ.Wrapper.Services;
using System;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace Goder.TestRunner
{
    class Program
    {
        private static IConfigurationRoot configuration { get; set; }
        private static QueueService queueService { get; set; }
        private static CommonQueueSettings queueSettings { get; set; }
        private static HubConnection connection { get; set; }

        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true);

            configuration = builder.Build();

            var rabbitMqOptions = configuration.GetSection("RabbitMq").Get<RabbitMQOptions>();
            var signalROptions = configuration.GetSection("SignalROptions").Get<SignalROptions>();

            using var prodFactory = new MessageFactory(rabbitMqOptions);
            queueSettings = prodFactory.GetQueueSettings(RabbitMQQueueNames.SEND_TESTS);

            queueService = new QueueService();
            queueService.ReceiveMessage += GetMessage;

            connection = new HubConnectionBuilder()
                .WithUrl(new Uri(signalROptions.URI + signalROptions.Hub))
                .WithAutomaticReconnect()
                .Build();

            await connection.StartAsync();
            Console.ReadLine();
        }

        private static void GetMessage(string message, ulong deliveryTag)
        {
            var problemTestsData = JsonConvert.DeserializeObject<ProblemTestsData>(message);

            var processOptions = configuration.GetSection("ProcessOptions").Get<ProcessOptions>();
            var testService = new TestService(processOptions);

            testService.SaveScriptInFile(AppDomain.CurrentDomain.BaseDirectory, problemTestsData.Script);

            var problemResult = new ProblemTestDataResponse();
            problemResult.Id = problemTestsData.Id;

            foreach(var test in problemTestsData.Tests)
            {
                var testResult = testService.StartTestInNewProcess(test);

                var isPassed = testService.CompareResults(test.Output, testResult);
                problemResult.IsPassed = isPassed;

                if (!isPassed)
                {
                    break;
                }
            }

            connection.InvokeAsync("ProblemResponse", problemResult).Wait();

            queueService.ReleaseRequest(queueSettings.Channel, deliveryTag);
        }
    }
}
