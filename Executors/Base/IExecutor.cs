public interface IExecutor
{
    public Task Compile(string code);

    public Task<string> Execute(string stdin); 
    public Task<string> Execute(); 
}