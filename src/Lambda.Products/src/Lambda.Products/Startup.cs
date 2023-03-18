using Auth.API.Configuration;
using Lambda.Products.Configuration;

namespace Lambda.Products
{
    [Amazon.Lambda.Annotations.LambdaStartup]
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", false)
                                    .Build();

            services.AddLambdaConfiguration(configuration);

            services.AddDynamoDBConfiguration(configuration);

            services.AddDependencyConfiguration(configuration);
        }
    }
}