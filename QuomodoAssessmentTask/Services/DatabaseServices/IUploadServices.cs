using QuomodoAssessmentTask.DTOs.Requests;
using QuomodoAssessmentTask.Models;

namespace QuomodoAssessmentTask.Services.DatabaseServices
{
    public interface IUploadServices
    {
        Task<Upload> UploadFile(UploadFilesRequest request, string url);
        Task<bool> DeleteFile(DeleteFileRequest request);
        Task<IEnumerable<Upload>> GetAllFiles();
    }
}
