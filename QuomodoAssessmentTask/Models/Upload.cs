using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuomodoAssessmentTask.Models
{
    public class Upload
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "File name cannot be more than 50 characters long.")]
        public string Name { get; set; }
        [Required]
        [ForeignKey("FolderId")]
        public int? FolderId { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        //Navigation Property
        public Folder Folder { get; set; }
    }
}
