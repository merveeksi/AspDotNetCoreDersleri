using BlogApp.Entity;

namespace BlogApp.Data.Abstract;

public interface ITagRepository
{
    IQueryable<Tag> Tags { get; } // IQueryable is used to query the database -ekstra kriterler ekleme
    
    void CreateTag(Tag tag);
}