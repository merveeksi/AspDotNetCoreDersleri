using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete;

public class EfUserRepository: IUserRepository
{
        private BlogContext _context;
        
        public EfUserRepository(BlogContext context)
        {
                _context = context; //context nesnesi oluşturuldu
        }
        
        public IQueryable<User> Users => _context.Users;

        public void CreateUser(User tag)
        {
                _context.Users.Add(tag); //post eklemek için
                _context.SaveChanges(); //ekleme işlemini kaydetmek için
        }
}