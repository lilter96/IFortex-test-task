using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Task 2: Returns the book with the highest total published value (Price * Copies).
    /// </summary>
    public async Task<Book> GetBook()
    {
        return await _context.Books
            .OrderByDescending(b => b.Price * b.QuantityPublished)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Task 1: Returns books with "Red" in the title and published after the release of "Carolus Rex" (22.05.2012).
    /// </summary>
    public async Task<List<Book>> GetBooks()
    {
        var carolusRexReleaseDate = new DateTime(2012, 5, 22);

        return await _context.Books
            .Where(b => b.Title.Contains("Red")
                        && b.PublishDate > carolusRexReleaseDate)
            .ToListAsync();
    }
}
