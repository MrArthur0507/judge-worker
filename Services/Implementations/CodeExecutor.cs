
using JudgeContracts;
using MassTransit.Contracts;

public class CodeExecutor : ICodeExecutor
{
    private readonly ILanguageDetector _languageDetector;
    private readonly ILogger _logger;
    public CodeExecutor(ILanguageDetector languageDetector, ILogger logger)
    {
        _languageDetector = languageDetector;
        _logger = logger;
    }

    public async Task<ExecuteCodeResult> ExecuteCode(ExecuteCode executeCode)
    {
        ExecuteCodeResult executeCodeResult = new ExecuteCodeResult();

        try
        {
            IExecutor executor = _languageDetector.GetExecutor(executeCode.Language);
            ExecuteCommandResult compileOutput = await executor.Compile(executeCode.Code);
            ExecuteCommandResult executionOutput = await executor.Execute();

            executeCodeResult.CompileResult = compileOutput.Output;
            executeCodeResult.CompileMeta = compileOutput.Meta;
            executeCodeResult.Output = executionOutput.Output;
            executeCodeResult.Meta = executionOutput.Meta;
            return executeCodeResult;
        }
        catch 
        {
            _logger.Log("Something went wrong with executing or compiling the code!");
            return new ExecuteCodeResult();
        }

    }
}