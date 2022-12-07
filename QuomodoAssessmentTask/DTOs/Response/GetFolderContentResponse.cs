using QuomodoAssessmentTask.Models;

namespace QuomodoAssessmentTask.DTOs.Response
{
    public class GetFolderContentResponse
    {
        public string Folder { get; set; }
        public List<Folder> SubFolders { get; set; }
        public List<Upload> Files { get; set; }
        public int FolderCount { get; set; }
        public int FileCount { get; set; }
    }
}
