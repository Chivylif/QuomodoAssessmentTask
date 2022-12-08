using System.ComponentModel.DataAnnotations;

namespace QuomodoAssessmentTask.DTOs.Requests
{
    public class GetFolderContentsRequest
    {
        public int FolderId { get; set; }
        public string FolderPath { get; set; } = "";
    }
}
