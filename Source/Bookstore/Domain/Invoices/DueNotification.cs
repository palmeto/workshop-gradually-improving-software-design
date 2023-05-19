using Bookstore.Domain.Common;
using Bookstore.Domain.Models;

namespace Bookstore.Domain.Invoices;

public record DueNotification(Customer customer, Money AmountDue);
