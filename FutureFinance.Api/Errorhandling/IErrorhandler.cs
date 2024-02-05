namespace FutureFinance.Api;

public interface IErrorhandler
{
    void LogError(Exception ex);
}
