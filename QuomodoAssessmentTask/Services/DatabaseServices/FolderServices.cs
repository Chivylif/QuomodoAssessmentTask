using QuomodoAssessmentTask.DTOs.Requests;
using QuomodoAssessmentTask.DTOs.Response;
using QuomodoAssessmentTask.Models;
using QuomodoAssessmentTask.Repository;
using System.Linq.Expressions;

namespace QuomodoAssessmentTask.Services.DatabaseServices
{
    public class FolderServices : IFolderServices
    {
        private readonly IGenericRepository<Folder> _folderRepo;
        private readonly IGenericRepository<Upload> _fileRepo;

        public FolderServices(IGenericRepository<Folder> folderRepo, IGenericRepository<Upload> fileRepo)
        {
            _folderRepo = folderRepo;
            _fileRepo = fileRepo;
        }

        public async Task<Folder> CreateFolder(string folderName)
        {
            var folder = new Folder
            {
                Name = folderName
            };

            var res = await _folderRepo.Create(folder);
            return res == true ? folder : null;
        }

        public async Task<Folder> CreateSubFolder(CreateSubFolderRequest request)
        {
            var folder = new Folder
            {
                Name = request.Name,
                ParentId = request.ParentId
            };

            var res = await _folderRepo.Create(folder);
            return folder;
        }

        public async Task<bool> DeleteFolder(DeleteFolderRequest request)
        {
            Expression<Func<Folder, bool>> where = f => f.Id == request.FolderId;

            var folder = await _folderRepo.GetById(where);
            var res = await _folderRepo.Delete(folder);

            return res;
        }

        public async Task<GetFolderContentResponse> GetFolderContents(int Id)
        {
            //Get Folder Details
            var folders = await _folderRepo.GetAll();
            var subFolders = folders.ToList().Where((x) => x.ParentId == Id);

            Expression<Func<Folder, bool>> where = (x) => x.Id == Id;
            Folder parentFolder = await _folderRepo.GetById(where);

            var folderCount = subFolders.Count();

            //Get File Details
            var allFiles = await _fileRepo.GetAll();
            var files = allFiles.ToList().Where((x) => x.FolderId == Id);

            var fileCount = files.Count();

            return new GetFolderContentResponse
            {
                Folder = parentFolder.Name,
                SubFolders = subFolders.ToList(),
                Files = files.ToList(),
                FolderCount = folderCount,
                FileCount = fileCount
            };
        }

        public async Task<bool> RenameFolder(RenameFolderRequest request)
        {
            Expression<Func<Folder, bool>> where = f => f.Id == request.FolderId;

            var folder = await _folderRepo.GetById(where);
            folder.Name = request.NewName;

            var res = await _folderRepo.Update(folder);

            return res;
        }
    }
}
