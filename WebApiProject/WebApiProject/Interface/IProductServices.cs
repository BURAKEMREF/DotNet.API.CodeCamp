using WebApiProject.Contracts;
using WebApiProject.Entities;

namespace WebApiProject.Interface
{
    public interface IProductServices
    {
        
        
            Task<IEnumerable<Product>> ProductGetAllAsync();
            Task<Product> ProductGetByIdAsync(Guid id);
            Task ProductCreateTodoAsync(CreateProductRequest request);
            Task ProductUpdateTodoAsync(Guid id, UpdateProductRequest request);
            Task ProductDeleteTodoAsync(Guid id);
        
    }
}
