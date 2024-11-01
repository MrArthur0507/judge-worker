
using MassTransit.Util;

public interface ILanguageDetector
{
    public IExecutor GetExecutor(string language);
     
}