namespace QuomodoAssessmentTask.DTOs.Requests
{
    public class DeleteFileRequest
    {
        public int FileId { get; set; }
        public string FolderPath { get; set; }
        public string FileName { get; set; }
    }
}
