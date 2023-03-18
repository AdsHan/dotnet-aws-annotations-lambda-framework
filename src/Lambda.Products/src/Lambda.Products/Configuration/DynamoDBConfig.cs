﻿
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;

namespace Lambda.Products.Configuration;

public static class DynamoDBConfiguration
{
    public static IServiceCollection AddDynamoDBConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var aa = configuration["AWS:AccessKey"];
        var bbb = configuration["AWS:SecretKey"];

        AWSOptions awsOptions = new AWSOptions
        {
            Credentials = new BasicAWSCredentials(configuration["AWS:AccessKey"], configuration["AWS:SecretKey"])
        };

        services.AddDefaultAWSOptions(awsOptions);

        services.AddAWSService<IAmazonDynamoDB>();

        services.AddScoped<IDynamoDBContext, DynamoDBContext>();

        return services;
    }
}
