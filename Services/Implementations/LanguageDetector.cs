using Microsoft.Extensions.DependencyInjection;

public class LanguageDetector : ILanguageDetector {
    private readonly IServiceProvider _serviceProvider;
    public LanguageDetector(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IExecutor GetExecutor(string language) {
        switch (language.ToLower())
        {
            case "c":
                return new CExecutor(_serviceProvider.GetRequiredService<Sandbox>());
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}