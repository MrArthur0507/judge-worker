
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
            string output = await _executor.ExecuteCode(context.Message);

            await context.RespondAsync<ExecuteCodeResult>(new ExecuteCodeResult() {
                Output = output
            }); 
        }

    }
}
