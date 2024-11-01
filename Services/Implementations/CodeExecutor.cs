
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

    public async Task<string> ExecuteCode(ExecuteCode executeCode)
    {

        try
        {
            IExecutor executor = _languageDetector.GetExecutor(executeCode.Language);
            await executor.Compile(executeCode.Code);
            return await executor.Execute();
        }
        catch 
        {
            _logger.Log("Something went wrong with executing or compiling the code!");
            return "Unable to execute code";
        }

    }
}