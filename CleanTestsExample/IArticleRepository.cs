using System;
using System.Threading.Tasks;

namespace CleanTestsExample
{
    /*
     * Repository for storing all (known) articles.
     */
    public interface IArticleRepository
    {
        Task<Article> GetById(Guid articleId);
    }
}