using QuomodoAssessmentTask.DTOs.Requests;

namespace QuomodoAssessmentTask.Services.ServerServices
{
    public class UploadServiceServer : IUploadServicesServer
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _rootPath;

        public UploadServiceServer(IWebHostEnvironment env)
        {
            _env = env;
            _rootPath = Path.Combine(_env.ContentRootPath, "wwwroot\\");
        }

        public async Task<string> UploadFile(UploadFilesRequest request)
        {

            var path = _rootPath + request.FolderPath + "\\" + request.Files.FileName;
            var fileUrl = $"{request.FolderPath}\\{request.Files.FileName}";

            if (File.Exists(path))
            {
                throw new Exception("File already exists");
            }
            
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await request.Files.CopyToAsync(stream);
            }

            return fileUrl;
        }

        public Task<bool> DeleteFile(DeleteFileRequest request)
        {
            var path = _rootPath + request.FolderPath + "\\" + request.FileName;

            if (File.Exists(path))
            {
                File.Delete(path);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
