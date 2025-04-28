using Microsoft.EntityFrameworkCore;
using BookShop.Models;
namespace BookShop.data{
    public class BookShopDbContext : DbContext{
        public BookShopDbContext(DbContextOptions<BookShopDbContext> options) :base(options){
            
        }
        public DbSet<Book> Book {get;set;}
    }
}