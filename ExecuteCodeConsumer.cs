using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeWorker
{
    public class ExecuteCodeConsumer : IConsumer<ExecuteCode>
    {
        public async Task Consume(ConsumeContext<ExecuteCode> context)
        {
            var message = context.Message;

            Console.WriteLine($"Processing Code: {message.Code} in Language: {message.Language}");

            await Task.CompletedTask;
        }
    }
}
