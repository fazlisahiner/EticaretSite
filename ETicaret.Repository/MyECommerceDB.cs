using ETicaret.Core.ETicaretModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Repository
{
    public class MyECommerceDB:DbContext
    {


        public MyECommerceDB()
        {
                
        }

        public MyECommerceDB(DbContextOptions<MyECommerceDB> options):base(options)
        {
                
        }

        public DbSet<User>  User{ get; set; }

        public DbSet<Address> Address{ get; set; }

        public DbSet<Category> Category{ get; set; }

        public DbSet<City> City{ get; set; }

        public DbSet<Country> Country{ get; set; }

        public DbSet<District> District{ get; set; }

        public DbSet<Emplooyee> Emplooyee { get; set; }

        public DbSet<Neighbourhood> Neighbourhood { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<OrderDetail> OrderDetail { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Town> Town { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//Model içinde tanımlanan Configuration ayarları için IEntityTypeConfiguration Interface yapısı Implement edilmiş. Bu da ApplyConfigurationsFromAssembly methodu ile beraber çalışır. Method içindeki tanımllanan yapı ile Configuration ayarları bütün Implement  alan class ların ayarlarını DB ye yansıtmasını sağlayacaktır

            base.OnModelCreating(modelBuilder);
        }





    }
}
