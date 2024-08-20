using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete;

public class EfCommentRepository: ICommentRepository
{
        private BlogContext _context;
        
        public EfCommentRepository(BlogContext context)
        {
                _context = context; //context nesnesi oluşturuldu
        }
        
        public IQueryable<Comment> Comments => _context.Comments; //commentleri getirme işlemi

        public void CreateComment(Comment comment)
        {
                _context.Comments.Add(comment); //post eklemek için
                _context.SaveChanges(); //ekleme işlemini kaydetmek için
        }
}