using AutoMapper;
using WebApiProject.Context;
using WebApiProject.Contracts;
using WebApiProject.Entities;
using WebApiProject.Interface;
using Microsoft.EntityFrameworkCore;

namespace WebApiProject.Services
{
    public class AdressService : IAdressServices
    {/*contextTodoDbContext: Veritabanıyla etkileşime geçmemizi sağlayan sınıfın bir örneği .
    _loggerILogger: Uygulamamız boyunca günlük kaydını kolaylaştıran sınıfın bir örneği .
    mapperIMapper: AutoMapper kullanarak nesneden nesneye eşleme yapmamızı sağlayan sınıfın bir örneği .*/
        private readonly WebContext _context;
        private readonly ILogger<AdressService> _logger;
        private readonly IMapper _mapper;
        public AdressService(WebContext context, ILogger<AdressService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task AdressCreateAsync(CreateAdressRequest request)
        {
            try
            {
                var adress = _mapper.Map<Adress>(request);
                
                _context.Adresses.Add(adress);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the Todo item.");
                throw new Exception("An error occurred while creating the Todo item.");
            }
        }

        public async Task AdressDeleteAsync(Guid id)
        {
            var adress = await _context.Adresses.FindAsync(id);
            if (adress != null)
            {
                _context.Adresses.Remove(adress);
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new Exception($"No  item found with the id {id}");
            }

        }

        public async Task<IEnumerable<Adress>> AdressGetAllAsync()
        {
            var adress = await _context.Adresses.ToListAsync();
            if (adress == null)
            {
                throw new Exception(" No Todo items found");
            }
            return adress;
        }

        public async  Task<Adress> AdressGetByIdAsync(Guid id)
        {
            var adress = await _context.Adresses.FindAsync(id);
            if (adress == null)
            {
                throw new KeyNotFoundException($"No Todo item with Id {id} found.");
            }
            return adress;
        }

        public async Task AdressUpdateAsync(Guid id, UpdateAdressRequest request)
        {
          
                try
                {
                    var adress = await _context.Adresses.FindAsync(id);
                    if (adress == null)
                    {
                        throw new Exception($"Adress item with id {id} not found.");
                    }

                    if (request.Title != null)
                    {
                    adress.Title = request.Title;
                    }

                    if (request.FullName != null)
                    {
                    adress.FullName = request.FullName;
                    }

                    if (request.Body != null)
                    {
                    adress.Body = request.Body;
                    }

                    if (request.UserID != null)
                    {
                    adress.UserID = request.UserID;
                    }
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while updating the Adress item with id {id}.");
                    throw;
                }
            }
    }
}
