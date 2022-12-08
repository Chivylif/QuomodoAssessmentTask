using QuomodoAssessmentTask.DTOs.Requests;
using QuomodoAssessmentTask.Models;
using QuomodoAssessmentTask.Repository;
using System.Linq.Expressions;

namespace QuomodoAssessmentTask.Services.DatabaseServices
{
    public class UploadServices : IUploadServices
    {
        private readonly IGenericRepository<Upload> _repo;

        public UploadServices(IGenericRepository<Upload> repo)
        {
            _repo = repo;
        }

        public async Task<Upload> UploadFile(UploadFilesRequest request, string url)
        {
            var upload = new Upload
            {
                Name = request.Files.FileName,
                FolderId = request.FolderId,
                FileUrl = url
            };

            var res = await _repo.Create(upload);
            return upload;
        }

        public async Task<bool> DeleteFile(DeleteFileRequest request)
        {
            Expression<Func<Upload, bool>> where = f => f.Id == request.FileId;

            var file = await _repo.GetById(where);
            var res = await _repo.Delete(file);

            return res;
        }
    }
}
