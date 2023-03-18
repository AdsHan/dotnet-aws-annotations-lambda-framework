namespace Lambda.Products.Configuration;

public static class LambdaConfiguration
{
    public static IServiceCollection AddLambdaConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(logging =>
        {
            logging.AddLambdaLogger();
            logging.SetMinimumLevel(LogLevel.Debug);
        });

        services.AddSingleton<IConfiguration>(configuration);

        return services;
    }
}
