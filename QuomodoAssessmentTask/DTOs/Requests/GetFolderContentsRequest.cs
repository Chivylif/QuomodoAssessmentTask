using System.ComponentModel.DataAnnotations;

namespace QuomodoAssessmentTask.DTOs.Requests
{
    public class GetFolderContentsRequest
    {
        [Required]
        public int FolderId { get; set; }
        public string FolderPath { get; set; }
    }
}
