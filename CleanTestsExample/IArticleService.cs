using System;
using System.Threading.Tasks;

namespace CleanTestsExample
{
    public interface IArticleService
    {
        Task<bool> IsPromoted(Guid articleId);
    }
}