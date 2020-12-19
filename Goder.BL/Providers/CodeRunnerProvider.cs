using AutoMapper;
using Goder.BL.DTO;
using Goder.BL.DTO.CodeRunning;
using Newtonsoft.Json;
using RabbitMQ.Wrapper.Models;
using RabbitMQ.Wrapper.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goder.BL.Providers
{
    public class CodeRunnerProvider
    {
        private readonly MessageFactory _messageFactory;
        private readonly QueueService _queueService;
        private readonly IMapper _mapper;

        public CodeRunnerProvider(MessageFactory messageFactory, QueueService queueService, IMapper mapper)
        {
            _messageFactory = messageFactory;
            _queueService = queueService;
            _mapper = mapper;
        }

        public void SendCodeToExecute(SolutionDTO solution)
        {
            var tests = _mapper.Map<ICollection<TestData>>(solution.Problem.Tests);
            var solutionTestData = new SolutionTestData()
            {
                Id = solution.Id,
                Script = solution.Script,
                Tests = tests
            };

            var senderSettings = _messageFactory.GetSenderSettings(RabbitMQQueueNames.SEND_TESTS, JsonConvert.SerializeObject(solutionTestData), true);

            _queueService.SendMessageToQueue(senderSettings);
        }
    }
}
