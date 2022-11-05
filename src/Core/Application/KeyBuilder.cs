namespace Foodstream.Application;

/// <summary>
/// Generating key-names for general purpose
/// </summary>
public class KeyBuilder
{                
    public string Key { get; private set;}
    public KeyBuilder(string prefix, string baseIdentifier, bool generateIdentifier = false)
    {
        Key = BuildKey(prefix, baseIdentifier, generateIdentifier);
    }

    private string BuildKey(string prefix, string baseIdentifier, bool generateIdentifier)
    {
        string uniqueIdentifier = generateIdentifier == true ? Guid.NewGuid().ToString() + "_" : "";
        return prefix + "/"  + uniqueIdentifier + baseIdentifier;
    }
}