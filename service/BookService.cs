
using BookShop.Models;
using BookShop.data;
using Microsoft.EntityFrameworkCore;
namespace ToDoApp.service;

public class BookService {
    private readonly BookShopDbContext _context;
    public BookService(BookShopDbContext context){
        _context = context;

    }
    public async Task<Book?> PatchBook(string Title,int Id){
        Console.WriteLine("PATCH : title"+Title);
        var book = await _context.Book.FirstOrDefaultAsync(b => b.Id == Id);
        if(book != null){
            book.Title = Title;
            await _context.SaveChangesAsync();
            return book;

        }
        return null;}

    public  async Task<Book?> DeleteBook(int Id){
        Console.WriteLine(""+Id);
        var book = _context.Book.FirstOrDefault(b => b.Id == Id);
        Console.WriteLine("book was selected ->"+book);
        if(book != null){
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return book;
        }
        return null;
    }
    public async Task<Book?> Create(Book item)
    {
        if (item == null)
        {
            return null;
        }
        await _context.Book.AddAsync(item);
        await _context.SaveChangesAsync();
        Console.WriteLine("add book was added");
        return item;
    }
    public async Task<List<Book>?> FetchBooks(){
    var books = await _context.Book.ToListAsync();
    return books;
}

}