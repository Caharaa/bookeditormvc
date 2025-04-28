
using Microsoft.AspNetCore.Mvc;
using BookShop.Models;
using BookShop.data;
using ToDoApp.service;
using System.Threading.Tasks;
namespace ToDoApp.Controllers;


public class HomeController : Controller
{


    private static List<Book> _book = new List<Book> { };
    private readonly BookService _bookservice;
    public HomeController(BookService bookservice,ILogger<HomeController> logger, BookShopDbContext context)
    {
        _bookservice = bookservice;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        _book = await _bookservice.FetchBooks();
        if(_book != null){
            return View(_book);
        }
        return BadRequest();
    }
    [HttpPost]
    public IActionResult Index(string SearchString)
    {
        var Result = _book.AsQueryable();
        if (!string.IsNullOrEmpty(SearchString))
        {
         Result = Result.Where(x => x.Title.Contains(SearchString) || x.Author.Contains(SearchString) || x.Year.Contains(SearchString));
         
        }
        return View(Result.ToList());
        
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Sloat()
    {
        return View();
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public  async Task<IActionResult> PostBook(Book book){
        var result = await _bookservice.Create(book);
        if(result != null){
            return RedirectToAction("index");
        }
        return BadRequest();
    }
    [HttpPost]
    public async Task<IActionResult> DeleteBook(int Id){
        var result = await _bookservice.DeleteBook(Id);
        if(result != null){
            return RedirectToAction("index");
        }
        return BadRequest();
    }
    [HttpPost]
    public async Task<IActionResult> PatchBook(string Title,int Id){
        var result = await _bookservice.PatchBook(Title,Id);
        if(result != null){
            return RedirectToAction("index");
        }
        return BadRequest();
    }

    /*[HttpPost]
    public IActionResult PutBook(Book book)
    {
        
        if (book == null)
        {
            return BadRequest("Invalid book data.");
        }

        var existingBook = db.Books.Find(book.Id);
        if (existingBook == null)
        {
            return NotFound();
        }

        existingBook.Title = book.Title;
        // Update other properties as needed

        db.SaveChanges();

        return RedirectToAction("Index"); // or any action you want to return to
    }*/

    /**[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }**/
}
