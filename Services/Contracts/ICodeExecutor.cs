using JudgeContracts;

public interface ICodeExecutor
{
    public Task<ExecuteCodeResult> ExecuteCode(ExecuteCode code);
}