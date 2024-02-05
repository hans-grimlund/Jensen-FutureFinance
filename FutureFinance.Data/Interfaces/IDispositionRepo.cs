using FutureFinance.Domain;

namespace FutureFinance.Data;

public interface IDispositionRepo
{
    void InsertDisposition(DispositionEntity disposition);
    List<DispositionEntity> GetDispositionsFromCustomer(int customerId);
    List<DispositionEntity> GetDispositionsFromAccount(int accountId);
    DispositionEntity GetDisposition(int id);
}
