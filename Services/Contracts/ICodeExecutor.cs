using JudgeContracts;

public interface ICodeExecutor
{
    public Task<string> ExecuteCode(ExecuteCode code);
}