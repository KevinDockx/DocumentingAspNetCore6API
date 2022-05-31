using Library.API.Contexts;
using Library.API.Entities;
using Microsoft.EntityFrameworkCore; 

namespace Library.API.Services
{
    public class BookRepository : IBookRepository
    {       
        private readonly LibraryContext _context; 

        public BookRepository(LibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentException("Argument can not be null or empty.", 
                    nameof(authorId));
            }

            return await _context.Books
                .Include(b => b.Author)
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<Book?> GetBookAsync(Guid authorId, Guid bookId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentException("Argument can not be null or empty.",
                    nameof(authorId));
            }

            if (bookId == Guid.Empty)
            {
                throw new ArgumentException("Argument can not be null or empty.",
                    nameof(bookId));
            }

            return await _context.Books
                .Include(b => b.Author)
                .Where(b => b.AuthorId == authorId && b.Id == bookId)
                .FirstOrDefaultAsync();
        }

        public void AddBook(Book bookToAdd)
        {
            if (bookToAdd == null)
            {
                throw new ArgumentNullException(nameof(bookToAdd));
            }

            _context.Add(bookToAdd);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // return true if 1 or more entities were changed
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
