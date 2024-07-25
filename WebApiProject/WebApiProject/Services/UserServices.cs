using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Context;
using WebApiProject.Contracts;
using WebApiProject.Entities;
using WebApiProject.Interface;


namespace WebApiProject.Services
{


    public class UserServices : IUserServices
    {
        private readonly WebContext _context;
        private readonly ILogger<UserServices> _logger;
        private readonly IMapper _mapper;

        //10
        public UserServices(
            WebContext context,
            ILogger<UserServices> logger,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /*_contextTodoDbContext: Veritabanıyla etkileşime geçmemizi sağlayan sınıfın bir örneği .
_loggerILogger: Uygulamamız boyunca günlük kaydını kolaylaştıran sınıfın bir örneği .
_mapperIMapper: AutoMapper kullanarak nesneden nesneye eşleme yapmamızı sağlayan sınıfın bir örneği .*/
        public async Task UserCreateTodoAsync(CreateUserRequest request)
        {
            try
            {
                var User = _mapper.Map<User>(request);
                User.CreatedAt = DateTime.UtcNow;
                _context.Add(User);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while creating the Todo item.");
                throw new Exception("An error occurred while creating the Todo item.");
            }
        }

        // Get all TODO Items from the database 
        public async Task<IEnumerable<User>> UserGetAllAsync()
        {
            var User = await _context.Users.ToListAsync();
            if (User == null)
            {
                throw new Exception(" No Users items found");
            }
            return User;

        }

        /*MappingCreateRequest : Nesneyi bir varlığa dönüştürmek için AutoMapper'ı kullanırız .
CreatedAtCreatedAt : Varlığın özelliğini Todogeçerli UTC tarih ve saatine ayarlıyoruz .
Veritabanına Ekleme : Varlığı bağlamımızdaki DbSet'e ekleriz ve değişiklikleri asenkron olarak kaydederiz .
Hata Yönetimi : İşlem sırasında oluşabilecek herhangi bir istisnayı yakalıyoruz, hatayı kaydediyoruz ve açıklayıcı bir hata mesajıyla yeni bir istisna atıyoruz.
         */
        //kaldırma işlemi burda döner.
        public async Task UserDeleteTodoAsync(Guid id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User != null)
            {
                _context.Users.Remove(User);
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new Exception($"No  item found with the id {id}");
            }

        }

        public async Task<User> UserGetByIdAsync(Guid id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null)
            {
                throw new KeyNotFoundException($"No Todo item with Id {id} found.");
            }
            return User;
        }

        public async Task UserUpdateTodoAsync(Guid id, UpdateUserRequest request)
        {
            
            
            try
            {
                var User = await _context.Users.FindAsync(id);
                if (User == null)
                {
                    throw new Exception($"Todo item with id {id} not found.");
                }

                if (request.Name != null)
                {
                    User.Name = request.Name;
                }

                if (request.Email != null)
                {
                    User.Email = request.Email;
                }

                if (request.Password != null)
                {
                    User.Password = request.Password;
                }

                if (request.CreatedAt != null)
                {
                    User.CreatedAt = request.CreatedAt;
                }

                if (request.Adresses != null)
                {
                    User.Adresses = request.Adresses;
                }
                if (request.UserName != null)
                {
                    User.UserName = request.UserName;
                }

                User.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the todo item with id {id}.");
                throw;
            }
            
        }
    }
}




