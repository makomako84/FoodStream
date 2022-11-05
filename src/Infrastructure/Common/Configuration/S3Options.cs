namespace Foodstream.Infrastructure.Configuration
{
    public class S3Options
    {
        public const string Section = "S3Settings";
        public string BucketName { get; set; }
        public string PointPrefix { get; set; }
    }
}