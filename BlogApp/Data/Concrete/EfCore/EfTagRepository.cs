using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete;

public class EfTagRepository: ITagRepository
{
        private BlogContext _context;
        
        public EfTagRepository(BlogContext context)
        {
                _context = context; //context nesnesi oluşturuldu
        }
        
        public IQueryable<Tag> Tags => _context.Tags;

        public void CreateTag(Tag tag)
        {
                _context.Tags.Add(tag); //post eklemek için
                _context.SaveChanges(); //ekleme işlemini kaydetmek için
        }
}