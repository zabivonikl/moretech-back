using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using MoretechBack.Database;

namespace MoretechBack;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration) => this.configuration = configuration;

    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<Context>();
        serviceCollection.AddSingleton(_ => new PolygonApi.Client(configuration["MoretechBack:PolygonApi"]));

        serviceCollection.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.SerializerOptions.AllowTrailingCommas = true;
                options.SerializerOptions.PropertyNameCaseInsensitive = true;
            }
        );
        
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

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}