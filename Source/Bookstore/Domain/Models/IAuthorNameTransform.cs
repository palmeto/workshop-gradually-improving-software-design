namespace Bookstore.Domain.Models;

public interface IAuthorNameTransform
{
    string Transform(Person author);
}
