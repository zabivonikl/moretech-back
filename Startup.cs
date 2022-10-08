using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using MoretechBack.Database;
using MoretechBack.PolygonApi;

namespace MoretechBack;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration) => this.configuration = configuration;

    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        string connectionString = configuration.GetConnectionString("MoretechBackDbConnectionString");
        serviceCollection.AddDbContext<ConnectionsContext>(options => options.UseNpgsql(connectionString));
        serviceCollection.AddSingleton<IPolygonApiClient>(_ => new Client(configuration["MoretechBack:PublicPolygonApi"]));

        serviceCollection.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.SerializerOptions.AllowTrailingCommas = true;
                options.SerializerOptions.PropertyNameCaseInsensitive = true;
            }
        );
        
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        
        serviceCollection.AddControllers();
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
        
        serviceCollection.AddCors(o => o.AddPolicy("AllPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ConnectionsContext connectionsContext)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        // connectionsContext.Database.Migrate();
        Console.WriteLine("Database migrated");
        

        app.UseAuthentication();
        
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        
        app.UseCors("AllPolicy");
        
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}