using QuomodoAssessmentTask.DTOs.Requests;

namespace QuomodoAssessmentTask.Services.ServerServices
{
    public interface IUploadServicesServer
    {
        Task<string> UploadFile(UploadFilesRequest request);
        Task<bool> DeleteFile(DeleteFileRequest request);
    }
}
