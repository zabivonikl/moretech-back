using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoretechBack.Auth;
using MoretechBack.Database;

namespace MoretechBack;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration) => this.configuration = configuration;

    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        string connectionString = configuration.GetConnectionString("MoretechBackDbConnectionString");
        serviceCollection.AddDbContext<ConnectionsContext>(options => options.UseNpgsql(connectionString));
        serviceCollection.AddSingleton(_ => new PolygonApi.Client(configuration["MoretechBack:PolygonApi"]));

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
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.UseAuthentication();
        
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}