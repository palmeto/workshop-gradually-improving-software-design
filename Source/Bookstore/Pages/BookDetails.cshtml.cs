using Bookstore.Domain.Common;
using Bookstore.Domain.Models;
using Bookstore.Domain.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookstore.Pages;

public class BookDetailsModel : PageModel
{
    public record PriceLine(string Label, Money Amount);

    private readonly ILogger<IndexModel> _logger;
    private readonly BookstoreDbContext _dbContext;
    private readonly Discounts _discounts;
    public Book Book { get; private set; } = null!;

    public IReadOnlyList<PriceLine> PriceSpecification { get; private set; } = Array.Empty<PriceLine>();

    public BookDetailsModel(ILogger<IndexModel> logger, BookstoreDbContext dbContext, Discounts discounts) =>
        (_logger, _dbContext, _discounts) = (logger, dbContext,discounts);

    public async Task<IActionResult> OnGet(Guid id)
    {
        if ((await _dbContext.Books.GetBooks().ById(id)) is Book book)
        {
            this.Book = book;
            this.PriceSpecification = new List<PriceLine>() { new("Price", BookPricing.SeedPriceFor(book, Currency.USD).Value) };
            UpdatePriceLinesWhenDiscounted();
            return Page();
        }

        return Redirect("/books");
    }

    private void UpdatePriceLinesWhenDiscounted()
    {
        if (!hasDiscount()) return;
        var original = this.PriceSpecification.First();
        List<PriceLine> updatedList = GetDiscountedPriceLines(original);
        this.PriceSpecification = updatedList;
    }

    private List<PriceLine> GetDiscountedPriceLines(PriceLine original)
    {
        var updatedList = new List<PriceLine>() { new("Original Price", original.Amount) };
        updatedList.Add(new PriceLine("** Discount **", new Money(original.Amount.Amount * (-_discounts.RelativeDiscount), Currency.USD)));
        updatedList.Add(new PriceLine("TOTAL", new Money(original.Amount.Amount * (1-_discounts.RelativeDiscount), Currency.USD)));
        return updatedList;
    }

    private bool hasDiscount()
    {
        return _discounts.RelativeDiscount <= 1 && _discounts.RelativeDiscount > 0;  
    }
}
