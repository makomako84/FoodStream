namespace Foodstream.Infrastructure.Common;

public class S3CredentialsOptions
{
    public const string Position = "S3Credentials";

    public string S3AccessKeyId { get; set; }

    public string S3SecretAccessKey { get; set; }

}
