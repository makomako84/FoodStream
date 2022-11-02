namespace Foodstream.Infrastructure.Common;

public class S3KeyBuilder
{                
    public string Key { get; private set;}
    public S3KeyBuilder(string prefix, string baseIdentifier, bool generateIdentifier = false)
    {
        Key = BuildKey(prefix, baseIdentifier, generateIdentifier);
    }

    private string BuildKey(string prefix, string baseIdentifier, bool generateIdentifier)
    {
        string uniqueIdentifier = generateIdentifier == true ? Guid.NewGuid().ToString() + "_" : "";
        return prefix + "/"  + uniqueIdentifier + baseIdentifier;
    }
}