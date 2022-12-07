using QuomodoAssessmentTask.DTOs.Requests;
using QuomodoAssessmentTask.DTOs.Response;
using QuomodoAssessmentTask.Models;

namespace QuomodoAssessmentTask.Services.DatabaseServices
{
    public interface IFolderServices
    {
        Task<GetFolderContentResponse> GetFolderContents(int Id);
        Task<Folder> CreateFolder(string folderName);
        Task<Folder> CreateSubFolder(CreateSubFolderRequest request);
        Task<bool> DeleteFolder(DeleteFolderRequest request);
        Task<bool> RenameFolder(RenameFolderRequest request);
    }
}

