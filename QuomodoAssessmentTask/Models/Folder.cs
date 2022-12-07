using System.ComponentModel.DataAnnotations;

namespace QuomodoAssessmentTask.Models
{
    public class Folder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Folder name cannot be more than 50 characters long.")]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
    }
}
