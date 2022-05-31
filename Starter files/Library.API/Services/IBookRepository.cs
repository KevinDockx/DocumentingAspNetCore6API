using Library.API.Entities;

namespace Library.API.Services
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync(Guid authorId);

        Task<Book?> GetBookAsync(Guid authorId, Guid bookId);

        void AddBook(Book bookToAdd);

        Task<bool> SaveChangesAsync();
    }
}
