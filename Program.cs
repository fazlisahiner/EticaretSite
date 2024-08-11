
using ETicaret.Core.IRepository;
using ETicaret.Core.IUnitOfWork;
using ETicaret.Repository;
using ETicaret.Repository.Repository;
using ETicaret.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Reflection;
//using ETicaret.Repository.Repository;

namespace EticaretSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            #region DB baglantisi

            builder.Services.AddDbContext<MyECommerceDB>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionDB"), option =>
                {
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(MyECommerceDB)).GetName().Name);
                });
            });



            #endregion

            // Add DI for repositories
           // builder.Services.AddScoped<IUser, UserRepository>();

           builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IUser, UserRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    // Di�er servisleri ekleyin
        //    services.AddScoped<IUser, UserRepository>();
        //}
    }
}
