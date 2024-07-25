using WebApiProject.Entities;
using WebApiProject.Contracts;
namespace WebApiProject.Interface
{
    public interface IAdressServices
    {
        Task<IEnumerable<Adress>> AdressGetAllAsync();
        Task<Adress> AdressGetByIdAsync(Guid id);
        Task AdressCreateAsync(CreateAdressRequest request);
        Task AdressUpdateAsync(Guid id, UpdateAdressRequest request);
        Task AdressDeleteAsync(Guid id);
    }
}
