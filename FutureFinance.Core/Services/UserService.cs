using FutureFinance.Data;
using FutureFinance.Domain;

namespace FutureFinance.Core;

public class UserService : IUserService
{
    private readonly ValidationService _validationService = new();
    private readonly MappingService _mappingService = new();
    private readonly UserRepo _userRepo = new();
    private readonly CustomerRepo _customerRepo = new();
    private readonly AuthorizationService _authorizationService = new();

    public LoginResponse Login(string email, string password)
    {
        var admin = _userRepo.GetAdmin(email);
        if (admin != null)
        {
            if (!BCrypt.Net.BCrypt.EnhancedVerify(password, admin.Password))
                return new(Status.Unauthorized);
            
            return new (Status.Ok, _authorizationService.GenerateToken(admin));
        }

        CustomerEntity customer = _customerRepo.GetCustomer(email);
        if (customer == null)
            return new(Status.NotFound);

        var user = _userRepo.GetUserFromCustomerId(customer.CustomerId);

        if (!BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password))
            return  new(Status.Unauthorized);
        
        return new(Status.Ok, _authorizationService.GenerateToken(user));
    }

    public Status AddUser(NewUserRequest user)
    {
        var status = _validationService.ValidateNewUser(user);
        if (status != Status.Ok)
            return status;

        user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password);
        var entity = _mappingService.ToUserEntity(user);
        entity.DateCreated = DateTime.Now;

        _userRepo.AddUser(entity);
        return Status.Ok;
    }

    public Status DeleteUser(int userId)
    {
        if (_userRepo.GetUser(userId) == null)
            return Status.NotFound;
        
        _userRepo.DeleteUser(userId);
        return Status.Ok;
    }

    public List<UserDTO> GetAllUsers()
    { 
        var entities = _userRepo.GetAllUsers();
        if (entities.Count < 1)
            return null!;

        return _mappingService.ToUserDTO(entities);
    }

    public UserDTO GetUser(int userId)
    {
        var entity = _userRepo.GetUser(userId);
        if (entity == null)
            return null!;
        
        return _mappingService.ToUserDTO(entity);
    }

    public UserDTO GetUserFromCustomerId(int customerId)
    {
        var entity = _userRepo.GetUserFromCustomerId(customerId);
        if (entity == null)
            return null!;
        
        return _mappingService.ToUserDTO(entity);
    }

    public Status UpdatePassword(string password, int userId)
    {
        if (_validationService.ValidatePassword(password) != Status.Ok)
            return Status.Invalid;
        
        _userRepo.UpdatePassword(BCrypt.Net.BCrypt.EnhancedHashPassword(password), userId);
        return Status.Ok;
    }
}
