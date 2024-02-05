namespace FutureFinance.Domain;

public enum Status
{
    None,
    Ok,
    NotFound,
    Forbidden,
    Unauthorized,
    Invalid,
    InvalidAmount,
    InvalidOperation,
    InvalidSymbol,
    InvalidBank,
    InvalidPassword,
    Empty,
    Error
}
