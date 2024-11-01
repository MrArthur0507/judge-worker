public interface IExecutor
{
    public Task Compile();

    public Task Execute(string stdin); 
}