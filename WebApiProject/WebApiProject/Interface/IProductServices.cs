using WebApiProject.Contracts;
using WebApiProject.Entities;

namespace WebApiProject.Interface
{
    public interface IProductServices
    {
        
        
            Task<IEnumerable<Product>> ProductGetAllAsync();
            Task<Product> ProductGetByIdAsync(int id);
            Task ProductCreateTodoAsync(CreateProductRequest request);
            Task ProductUpdateTodoAsync(int id, UpdateProductRequest request);
            Task ProductDeleteTodoAsync(int id);
        
    }
}
