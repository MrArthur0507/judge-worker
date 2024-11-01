
using JudgeContracts;
using MassTransit;

namespace JudgeContracts.ExecuteCodeConsumer
{
    public class ExecuteCodeConsumer : IConsumer<ExecuteCode>
    {
        private readonly ICodeExecutor _executor;
        public ExecuteCodeConsumer(ICodeExecutor executor)
        {
            _executor = executor;
        }
        public async Task Consume(ConsumeContext<ExecuteCode> context)
        {   
            await _executor.ExecuteCode(context.Message);
        }

    }
}
