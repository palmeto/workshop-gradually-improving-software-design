using Bookstore.Domain.Common;

namespace Bookstore.Domain.Models;

public interface IDiscount
{
    IEnumerable<DiscountApplication> GetDiscountAmount(Money price);
}

public record DiscountApplication(string Label, Money Amount);

public class NoDiscount : IDiscount
{
    public IEnumerable<DiscountApplication> GetDiscountAmount(Money price) =>
        Enumerable.Empty<DiscountApplication>();
}

public class RelativeDiscount : IDiscount
{
    private readonly decimal _factor;

    public RelativeDiscount(decimal factor) => _factor = factor>0  && factor < 1 ? factor : throw new ArgumentException();

    public IEnumerable<DiscountApplication> GetDiscountAmount(Money price)
    {
       yield return new ("Discount..", price * _factor);
    }

    public static IDiscount Create(decimal factor) => 
        factor <=0 && factor >= 1 ?
        new NoDiscount() : new RelativeDiscount(factor);
}
