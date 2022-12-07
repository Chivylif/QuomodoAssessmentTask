using QuomodoAssessmentTask.DTOs.Requests;
using QuomodoAssessmentTask.DTOs.Response;
using QuomodoAssessmentTask.Models;
using QuomodoAssessmentTask.Repository;

namespace QuomodoAssessmentTask.Services.DatabaseServices
{
    public class FolderServices : IFolderServices
    {
        private readonly IGenericRepository<Folder> _repo;
                                                               
        public FolderServices(IGenericRepository<Folder> repo)
        {
            _repo = repo;
        }

        public async Task<GetFolderContentResponse> GetFolderContents(GetFolderContentsRequest request)
        {
            var folders = await _repo.GetAll();
            var files = await _repo.GetAll();
            var folderCount = folders.Count();
            var fileCount = files.Count();
            return new GetFolderContentResponse
            {
                Folders = folders,
                Files = files,
                FolderCount = folderCount,
                FileCount = fileCount
            };
        }
    }
}
