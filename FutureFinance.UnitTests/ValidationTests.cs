using Xunit;
using FutureFinance.Core;
using FutureFinance.Domain;

namespace FutureFinance.UnitTests;

public class ValidationTests
{
    [Fact]
    public void ValidatePassword_NotEmpty_Ok()
    {
        var validationService = new ValidationService();
        var password = "123456789";

        var result = validationService.ValidatePassword(password);

        Assert.Equal(Status.Ok, result);
    }

    [Fact]
    public void ValidatePassword_EmptyString_InvalidPassword()
    {
        var validationService = new ValidationService();
        var password = string.Empty;

        var result = validationService.ValidatePassword(password);

        Assert.Equal(Status.InvalidPassword, result);
    }
    
    [Fact]
    public void ValidatePassword_PasswordToLong_InvalidPassword()
    {
        var validationService = new ValidationService();
        var password = "123456789012345678901234567890123456789012345678901234567890";

        var result = validationService.ValidatePassword(password);

        Assert.Equal(Status.InvalidPassword, result);
    }

    [Fact]
    public void ValidatePassword_PasswordToShort_InvalidPassword()
    {
        var validationService = new ValidationService();
        var password = "12345";

        var result = validationService.ValidatePassword(password);

        Assert.Equal(Status.InvalidPassword, result);
    }

    [Fact]
    public void ValidateTransaction_ValidTransaction_Ok()
    {
        var validationService = new ValidationService();
        NewTransactionRequest request = new(10, "Credit in cash", 500, "", "FF", 921);

        var result = validationService.ValidateTransaction(request);

        Assert.Equal(Status.Ok, result);
    }

    [Fact]
    public void ValidateTransaction_InvalidBank_InvalidBank()
    {
        var validationService = new ValidationService();
        NewTransactionRequest request = new(10, "Credit in cash", 500, "", "SEB", 921);

        var result = validationService.ValidateTransaction(request);

        Assert.Equal(Status.InvalidBank, result);
    }

    [Fact]
    public void ValidateTransaction_InvalidOperation_InvalidOperation()
    {
        var validationService = new ValidationService();
        NewTransactionRequest request = new(10, string.Empty, 500, "", "FF", 921);

        var result = validationService.ValidateTransaction(request);

        Assert.Equal(Status.InvalidOperation, result);
    }

    [Fact]
    public void ValidateTransaction_InvalidAmount_InvalidAmount()
    {
        var validationService = new ValidationService();
        NewTransactionRequest request = new(10, "Credit in cash", 0, "", "FF", 921);

        var result = validationService.ValidateTransaction(request);

        Assert.Equal(Status.InvalidAmount, result);
    }
    
}