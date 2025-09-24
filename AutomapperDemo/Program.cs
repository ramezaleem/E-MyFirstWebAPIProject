
using AutomapperDemo.Data;
using Microsoft.EntityFrameworkCore;

namespace AutomapperDemo;

public class Program
{
    public static void Main ( string[] args )
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // Â‰« „⁄‰Ì‰ ≈‰ Œ«’Ì… «·√”„«¡ Â ›÷· “Ì „« ÂÌ ›Ì Json° „‘ ÂÌ €Ì— «·ﬂÌ”‰Ã
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

        builder.Services.AddDbContext<ProductDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDBConnection")));


        builder.Services.AddDbContext<UserDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("UsersDBConnection")));


        builder.Services.AddDbContext<EmployeeDBContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDBConnection")));

        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
