namespace Monetrix.InterFaces
{
    public interface IUploadFile
    {
        Task<string> UploadFileAsync(string filePath, IFormFile file);
    }
}
