using FutbolowaJaskinia.Models;
using Microsoft.EntityFrameworkCore;

namespace FutbolowaJaskinia.Data
{
    public class UtiDbContext : DbContext
    {
        public UtiDbContext(DbContextOptions<UtiDbContext> opts) : base(opts)
        {

        }

        public DbSet<HighlightsModel> Highlights { get; set; }
        public DbSet<NewsModel> News { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<ChatModel> Chats { get; set; }
    }
}
