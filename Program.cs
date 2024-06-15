
using ETicaret.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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


            #region DB baðlantýsý

            builder.Services.AddDbContext<MyECommerceDB>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionDB"), option =>
                {
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(MyECommerceDB)).GetName().Name);
                });
            });



            #endregion



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
    }
}
