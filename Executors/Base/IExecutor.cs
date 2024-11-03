public interface IExecutor
{
    public Task<ExecuteCommandResult> Compile(string code);

    public Task<ExecuteCommandResult> Execute(string[] stdin); 
    public Task<ExecuteCommandResult> Execute(); 
}