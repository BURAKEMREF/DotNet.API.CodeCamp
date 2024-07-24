using WebApiProject.Entities;
using WebApiProject.Contracts;
namespace WebApiProject.Interface
{
    public interface IUserServices
    {
        Task<IEnumerable<User>> UserGetAllAsync();
        Task<User> UserGetByIdAsync(Guid id);
        Task UserCreateTodoAsync(CreateUserRequest request);
        Task UserUpdateTodoAsync(Guid id, UpdateUserRequest request);
        Task UserDeleteTodoAsync(Guid id);
    }
}

