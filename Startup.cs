using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MyApi.Repositories;
using MyApi.Services;
using MyApi.Models;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<MongoDbSettings>(
            options =>
            {
                options.ConnectionString = "mongodb://localhost:27017/TicketDB";
                options.Database = "TicketDB"; 
            });
        
        services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });
        
        services.AddScoped<ITicketRepository, TicketRepository>();
        
        services.AddScoped<TicketService>();

        // Add MVC services
        services.AddControllers();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
