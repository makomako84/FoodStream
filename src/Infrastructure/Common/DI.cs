using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Extensions.NETCore.Setup;
using MakoSystems.Foodstream.Application;

namespace MakoSystems.Foodstream.Common;

public static class DI
{
    public static IServiceCollection AddInfrastracture(
        this IServiceCollection services,
        ConfigurationManager configuration
        )
    {
        // s3
        var s3Credentials = configuration
            .GetSection(S3CredentialsOptions.Position)
            .Get<S3CredentialsOptions>();
        var s3Options = configuration.GetAWSOptions();
        s3Options.Credentials = new Amazon.Runtime.BasicAWSCredentials(
            s3Credentials.S3AccessKeyId,
            s3Credentials.S3SecretAccessKey);
        services.AddDefaultAWSOptions(s3Options);
        services.AddAWSService<IAmazonS3>();

        services
            .Configure<InfrastractureSettings>(configuration.GetSection(InfrastractureSettings.Position));

        services.AddScoped<IS3Service, S3Service>();

        return services;
    }
}
