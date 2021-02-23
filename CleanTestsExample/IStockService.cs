using System;
using System.Threading.Tasks;

namespace CleanTestsExample
{
    /*
     * Imaginary service for checking stock quantity
     * for a given article ID.
     */
    public interface IStockService
    {
        Task<int> GetQuantity(Guid articleId);
    }
}