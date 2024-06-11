using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Kolok2.Context;
using Kolok2.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        //Registering services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers().AddJsonOptions(x => 
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        builder.Services.AddDbContext<ContextDB>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        
        builder.Services.AddScoped<IDbService, DbService>();
        
        var app = builder.Build();

        //Configuring the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}