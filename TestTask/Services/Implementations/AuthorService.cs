using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly ApplicationDbContext _context;

    public AuthorService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Task 4: Returns the author who wrote the book with the longest title.
    /// If there are multiple authors, return the one with the smallest Id.
    /// </summary>
    public async Task<Author> GetAuthor()
    {
        var maxTitleLength = await _context.Books
            .MaxAsync(b => b.Title.Length);

        var author = await _context.Authors
            .Where(a => a.Books.Any(b => b.Title.Length == maxTitleLength))
            .OrderBy(a => a.Id)
            .FirstOrDefaultAsync();

        return author!;
    }

    /// <summary>
    /// Task 3: Returns authors who wrote an even number of books published after 2015.
    /// </summary>
    public async Task<List<Author>> GetAuthors()
    {
        var authors = await _context.Authors
            .Where(a =>
                a.Books.Count(b => b.PublishDate.Year > 2015) % 2 == 0
            )
            .ToListAsync();

        return authors;
    }
}
