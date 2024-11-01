
using JudgeContracts;
using MassTransit;

namespace JudgeContracts.ExecuteCodeConsumer
{
    public class ExecuteCodeConsumer : IConsumer<ExecuteCode>
{
    public Task Consume(ConsumeContext<ExecuteCode> context)
    {
        throw new NotImplementedException();
    }
}
}
