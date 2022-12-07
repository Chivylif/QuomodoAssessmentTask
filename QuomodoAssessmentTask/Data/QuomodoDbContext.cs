using Microsoft.EntityFrameworkCore;
using QuomodoAssessmentTask.Models;

namespace QuomodoAssessmentTask.Data
{
    public class QuomodoDbContext : DbContext
    {
        public QuomodoDbContext(DbContextOptions<QuomodoDbContext> options) : base(options)
        {
        }

        public DbSet<Folder> Folders { get; set; }
        public DbSet<Upload> Uploads { get; set; }
    }
}
