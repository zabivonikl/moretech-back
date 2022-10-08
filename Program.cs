using MoretechBack;

static IHostBuilder CreateHostBuilder(string[] args) => Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
CreateHostBuilder(args).Build().Run();