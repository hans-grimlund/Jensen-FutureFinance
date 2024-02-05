using FutureFinance.Domain;

namespace FutureFinance.Data;

public interface IUserRepo
{
    void AddUser(UserEntity user);
    void UpdatePassword(string password, int userId);
    void DeleteUser(int userId);
    UserEntity GetUser(int userId);
    UserEntity GetUserFromCustomerId(int customerId);
    List<UserEntity> GetAllUsers();
    UserEntity GetAdmin(string adminlogin);
}
