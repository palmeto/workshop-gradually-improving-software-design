namespace Bookstore.Domain.Models;

public interface IBookTransform
{
    string Transform(Person author);
}