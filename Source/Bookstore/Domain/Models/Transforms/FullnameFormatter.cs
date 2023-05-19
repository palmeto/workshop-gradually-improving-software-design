namespace Bookstore.Domain.Models.Transforms
{
    public class FullnameFormatter : IAuthorNameTransform
    {
        public string Transform(Person author)
        {
            return $"{author.LastName}, {author.FirstName}";
        }
    }
}
