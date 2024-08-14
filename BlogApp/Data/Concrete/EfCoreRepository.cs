using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete;

public class EfCoreRepository: IPostRepository
{
        private BlogContext _context;
        
        public EfCoreRepository(BlogContext context)
        {
                _context = context; //context nesnesi oluşturuldu
        }
        
        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
                _context.Posts.Add(post); //post eklemek için
                _context.SaveChanges(); //ekleme işlemini kaydetmek için
        }
}