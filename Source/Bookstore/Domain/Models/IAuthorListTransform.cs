namespace Bookstore.Domain.Models;

public interface IAuthorListTransform
{
    string Transform(IEnumerable<Person> authors);
}
