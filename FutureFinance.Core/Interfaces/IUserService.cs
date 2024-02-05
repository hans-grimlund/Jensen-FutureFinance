using FutureFinance.Domain;

namespace FutureFinance.Core;

public interface IUserService
{
    LoginResponse Login(string email, string password);
    Status AddUser(NewUserRequest user);
    Status UpdatePassword(string password, int userId);
    Status DeleteUser(int userId);
    UserDTO GetUser(int userId);
    UserDTO GetUserFromCustomerId(int customerId);
    List<UserDTO> GetAllUsers();
}
