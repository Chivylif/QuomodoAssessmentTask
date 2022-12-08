using QuomodoAssessmentTask.DTOs.Requests;
using QuomodoAssessmentTask.DTOs.Response;
using QuomodoAssessmentTask.Models;

namespace QuomodoAssessmentTask.Services.ServerServices
{
    public interface IFolderServicesServer
    {
        Task<GetFolderContentResponse> GetFolderContents(GetFolderContentsRequest request);
        Task<bool> CreateFolder(string folderName);
        Task<bool> CreateSubFolder(CreateSubFolderRequest request);
        Task<bool> DeleteFolder(DeleteFolderRequest request);
        Task<bool> RenameFolder(RenameFolderRequest request);
    }
}
    