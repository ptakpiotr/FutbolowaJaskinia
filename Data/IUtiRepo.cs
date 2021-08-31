using FutbolowaJaskinia.Models;
using System.Collections.Generic;

namespace FutbolowaJaskinia.Data
{
    public interface IUtiRepo
    {
        List<HighlightsModel> GetHighlights();
        void AddHighlights(IEnumerable<HighlightsModel> highlights);
        void SaveChanges();

        List<NewsModel> GetNews();
        NewsModel GetOneNews(string id);
        void AddNews(NewsModel news);
        void UpdateNews(NewsModel news);

        List<CommentModel> GetComments(string postId);
        void AddComment(CommentModel comment);
        void DeleteComment(string commId);

        List<ChatModel> GetAllChat();
        void AddChat(ChatModel chat);
        void DeleteChat();
    }
}
