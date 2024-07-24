using LiteDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApiProject.Entities;


namespace WebApiProject.Context
{//ctrl k + d hizalandırr.
    public class WebContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        

        public WebContext(DbContextOptions<WebContext> options) : base(options)
        {

        }
        private readonly DbContext _dbContext;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "dbo");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category", "dbo");
            });
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> orders { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Adress> Adresses { get; set; }









    }

    


      
    }

