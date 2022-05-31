using Library.API.Entities;

namespace Library.API.Services
{
    public interface IAuthorRepository
    {
        Task<bool> AuthorExistsAsync(Guid authorId);

        Task<IEnumerable<Author>> GetAuthorsAsync();

        Task<Author?> GetAuthorAsync(Guid authorId);

        void UpdateAuthor(Author author);

        Task<bool> SaveChangesAsync();
    }
}
