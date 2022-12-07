using System.ComponentModel.DataAnnotations;

namespace QuomodoAssessmentTask.DTOs.Requests
{
    public class CreateSubFolderRequest
    {
        [Required]
        public string Name { get; set; }
        public int ParentId { get; set; }

        [Required]
        public string ParentFolderPath { get; set; }
    }
}
