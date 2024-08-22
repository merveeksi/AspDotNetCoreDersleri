using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete;

public class EfPostRepository: IPostRepository
{
        private BlogContext _context;
        
        public EfPostRepository(BlogContext context)
        {
                _context = context; //context nesnesi oluşturuldu
        }
        
        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)
        {
                _context.Posts.Add(post); //post eklemek için
                _context.SaveChanges(); //ekleme işlemini kaydetmek için
        }

        public void EditPost(Post post)
        {
                var entity = _context.Posts.FirstOrDefault(i => i.PostId == post.PostId); //post id'sine göre postu bul

                if (entity != null)
                {
                        entity.Title = post.Title;
                        entity.Description = post.Description;
                        entity.Content = post.Content;
                        entity.Url = post.Url;
                        entity.IsActive = post.IsActive;
                        
                        _context.SaveChanges(); //değişiklikleri kaydet
                }
        }
        
        public void EditPost(Post post, int[] tagIds)
        {
                var entity = _context.Posts.Include(i=>i.Tags).FirstOrDefault(i => i.PostId == post.PostId); //post id'sine göre postu bul

                if (entity != null)
                {
                        entity.Title = post.Title;
                        entity.Description = post.Description;
                        entity.Content = post.Content;
                        entity.Url = post.Url;
                        entity.IsActive = post.IsActive;

                        entity.Tags = _context.Tags.Where(tag => tagIds.Contains(tag.TagId)).ToList(); //tagları güncelle //eşleşen tagları al
                        
                        _context.SaveChanges(); //değişiklikleri kaydet
                }
        }
}