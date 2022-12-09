namespace Foodstream.Presentation.Contracts
{
    public class FileDownloadResponse
    {    
        public byte[] File { get; set; }
        public string FileName { get; set; }
    }
}
