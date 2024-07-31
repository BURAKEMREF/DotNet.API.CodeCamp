using WebApiProject.Entities;
using WebApiProject.Contracts;
namespace WebApiProject.Interface
{
    public interface IUserServices
    {
        Task<IEnumerable<User>> UserGetAllAsync();
        Task<User> UserGetByIdAsync(long id);
        Task UserCreateTodoAsync(CreateUserRequest request);
        Task UserUpdateTodoAsync(long id, UpdateUserRequest request);
        Task UserDeleteTodoAsync(Guid id);
    }
}

