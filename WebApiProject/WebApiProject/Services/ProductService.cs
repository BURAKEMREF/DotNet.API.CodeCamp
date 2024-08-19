using AutoMapper;
using WebApiProject.Context;
using WebApiProject.Contracts;
using WebApiProject.Entities;
using WebApiProject.Interface;
using Microsoft.EntityFrameworkCore;

namespace WebApiProject.Services
{
   
    
        public class ProductService : IProductServices
    {
            private readonly WebContext _context;
            private readonly ILogger<ProductService> _logger;
            private readonly IMapper _mapper;

            //10
            public ProductService(
                WebContext context,
                ILogger<ProductService> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

        /*_contextTodoDbContext: Veritabanıyla etkileşime geçmemizi sağlayan sınıfın bir örneği .
_loggerILogger: Uygulamamız boyunca günlük kaydını kolaylaştıran sınıfın bir örneği .
_mapperIMapper: AutoMapper kullanarak nesneden nesneye eşleme yapmamızı sağlayan sınıfın bir örneği .*/

        // Get all Products Items from the database 
        public async Task<IEnumerable<Product>> ProductGetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            if (products == null)
            {
                throw new Exception(" No Todo items found");
            }
            return products;

        }

        /*Task<IEnumerable<Product>> IProductServices.ProductGetAllAsync()
        {

            throw new NotImplementedException();


        }
        */
   
       public async  Task<Product> ProductGetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"No Todo item with Id {id} found.");
            }
            return product;
        }


        public async Task ProductCreateTodoAsync(CreateProductRequest request)
        {
            try
            {
                var product = _mapper.Map<Product>(request);
                
                _context.Add(product);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while creating the Todo item.");
                throw new Exception("An error occurred while creating the Todo item.");
            }
        }
        /*Belirli Bir Yapılacaklar Öğesini GetirmeFindAsync : Bir Yapılacaklar öğesini . yoluyla getirmek için Entity Framework Core'un yöntemini kullanırız Id.

Yapılacak Öğesini Güncelleme : Nesnede sağlanan değerlere göre Yapılacak öğesinin özelliklerini güncelliyoruz UpdateTodoRequest.

Hata İşleme : Belirtilen Todo öğesi bulunamazsa Id, açıklayıcı bir hata mesajıyla bir istisna atarız.*/
        public async Task ProductUpdateTodoAsync(int id, UpdateProductRequest request)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    throw new Exception($"Todo item with id {id} not found.");
                }

                //if (request.ProductId != null)
                //{
                //    product.ProductId = request.ProductId;
                //}

                if (request.Name != null)
                {
                    product.Name = request.Name;
                }

                if (request.Price != null)
                {
                    product.Price = request.Price;
                }if (request.Name != null)
                {
                    product.Name = request.Name;
                }if (request.Description != null)
                {
                    product.Description = request.Description;
                }if (request.Status != null)
                {
                    product.Status = request.Status;
                }if (request.Category != null)
                {
                    product.Category = request.Category;
                }

             

            
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the todo item with id {id}.");
                throw;
            }

        }

        public async Task ProductDeleteTodoAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new Exception($"No  item found with the id {id}");
            }
        }
   
    
    
    }
    }



