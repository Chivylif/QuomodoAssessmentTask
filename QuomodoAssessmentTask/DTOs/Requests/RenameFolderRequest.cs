using System.ComponentModel.DataAnnotations;

namespace QuomodoAssessmentTask.DTOs.Requests
{
    public class RenameFolderRequest
    {
        [Required]
        public string OldName { get; set; }
        [Required]
        public string NewName { get; set; }
        [Required]
        public int FolderId { get; set; }
        public string ParentFolderPath { get; set; }
    }
}
