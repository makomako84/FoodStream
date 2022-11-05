namespace Foodstream.Infrastructure.Configuration
{
    /// <summary>
    /// Конфигурация подключения к хранилищу amazon
    /// </summary>
    public class S3CredentialsOptions
    {
        public const string Section = "S3Credentials";

        /// <summary>
        /// Credentials ключ
        /// </summary>
        public string S3AccessKeyId { get; set; }

        /// <summary>
        /// Credentials ключ
        /// </summary>
        public string S3SecretAccessKey { get; set; }
    }
}
