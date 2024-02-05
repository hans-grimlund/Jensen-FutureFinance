namespace FutureFinance.Api;

public class Errorhandler(ILogger<Errorhandler> logger) : IErrorhandler
{
    private readonly ILogger<Errorhandler> _log = logger;

    public void LogError(Exception ex)
    {
        _log.LogError(ex.Message);
        _log.LogInformation(ex.StackTrace);
    }
}
