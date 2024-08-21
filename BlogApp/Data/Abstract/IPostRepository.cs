using BlogApp.Entity;

namespace BlogApp.Data.Abstract;

public interface IPostRepository
{
    IQueryable<Post> Posts { get; } // IQueryable is used to query the database -ekstra kriterler ekleme
    
    void CreatePost(Post post);
    void EditPost(Post post);
}