using Monetrix.InterFaces;

namespace Monetrix.Repository
{
    public class UploadFile : IUploadFile
    {
        private IWebHostEnvironment _environment;

        public UploadFile(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFileAsync(string filePath, IFormFile file)
        {
            string upLoadFolder = _environment.WebRootPath + filePath;
            if (Directory.Exists(upLoadFolder) == false)
            {
                Directory.CreateDirectory(upLoadFolder);
            }
            string UniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string FullPath = Path.Combine(upLoadFolder, UniqueFileName);
            using (var stream = new FileStream(FullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filePath + UniqueFileName;
        }
    }
}
