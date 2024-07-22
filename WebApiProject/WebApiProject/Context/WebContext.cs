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

    class Program
    {
        static void GetSqlConnection()
        {
            String connectionString = @"Data Source=.\SQLEXPRESS;.MDF;Initial Catalog=Alrakis;Integrated Security=SSPI";
            using (var connection = new SqlConnection(connectionString)) ;
        }



        //static void DeleteProduct(int id)
        //{

        //    using (var db = new WebContext())
        //    {
        //        var p = db.Products.FirstOrDefault(p => p.Id == id);
        //        if (p == null)
        //        {
        //            db.Remove(p);
        //        }
        //    }
        //}

        //// change tracking
        //static void UptadeProduct()
        //{
        //    using (var db = new WebContext())
        //    {
        //        var p = db.Products.Where(i => i.Id == 1).FirstOrDefault();
        //        if (p != null)
        //        {
        //            p.Price *= 1.2m;
        //            db.SaveChanges();
        //        }
        //    }

        //    static void GetProductById(int id)
        //    {
        //        using (var context = new WebContext())
        //        {
        //            var result = context.Products.Where(p => p.Id == id);

        //        }
        //    }

        //    // ürünleri data baseden çağırma
        //    static void GetAllProducts()
        //    {
        //        using (var context = new WebContext())
        //        {
        //            var products = context.Products.ToList();
        //            foreach (var p in products)
        //            {
        //                Console.WriteLine($"name:{p.Name} price:{p.Price}");
        //            }
        //        }
        //    }

        //    static void AddProduct()
        //    {
        //        using (var db = new WebContext())

        //        {
        //          //  var products = new List<Product>()
        //        {
        //           // new Product{ Name = "samsung7", Price = 2000},
        //            //new Product{ Name = "samsung8", Price = 5000},
        //            //new Product{ Name = "samsung9", Price = 4000},
        //           //new Product{ Name = "samsung1", Price = 45000}
        //        };
        //            //foreach (var a in products) { db.Products.Add(a); }
        //           // db.Products.AddRange(products); // hepsini ekler RAnge bize bunu sağlar
        //            //var p = new Product { Name = "samsung", Price = 2000 };
        //            //db.Products.Add(p);
        //            db.SaveChanges();
        //            Console.WriteLine("Veriler yazıldı");
        //        }


        //    }
        //}
    }
}
