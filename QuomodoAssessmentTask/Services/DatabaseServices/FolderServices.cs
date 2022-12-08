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
            //Delete all files in folder
            var filesToDelete = new List<Upload>();
            
            Expression<Func<Upload, bool>> where2 = f => f.FolderId == request.FolderId;
            var files = await _fileRepo.GetAll();
            if (files != null)
            {
                filesToDelete = files.Where(where2.Compile()).ToList();
            }
            
            if (filesToDelete != null)
            {
                foreach (var file in filesToDelete)
                {
                    await _fileRepo.Delete(file);
                }
            }

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
            var folderNames = new List<string>();
            
            foreach (var folder in subFolders)
            {
                folderNames.Add(folder.Name);
            }

            var folderCount = subFolders.Count();

            //Get File Details
            var allFiles = await _fileRepo.GetAll();
            var files = allFiles.ToList().Where((x) => x.FolderId == Id);
            var fileNames = new List<string>();

            foreach (var file in files)
            {
                fileNames.Add(file.Name);
            }

            var fileCount = files.Count();

            return new GetFolderContentResponse
            {
                Folder = parentFolder.Name,
                SubFolders = folderNames,
                Files = fileNames,
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
