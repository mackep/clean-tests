using System;
using System.Threading.Tasks;

namespace CleanTestsExample
{
    /*
     * Primary unit in this example project to which all the tests apply.
     *
     * Contains logic to determine if an article (referenced by ID) in an
     * imaginary e-commerce site should be promoted or not. A promoted 
     * article could for example indicate that an article should be
     * highlighted or visually promoted on a site.
     */
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IStockService _stockService;

        public ArticleService(IStockService stockService,
            IArticleRepository articleRepository)
        {
            _stockService = stockService;
            _articleRepository = articleRepository;
        }

        /*
         * An article should be promoted if it:
         * - Is a known article
         * - Is on sale (has a discount percentage > 0)
         * - Has a good margin (>= 5 percent)
         * - Is in stock
         */
        public async Task<bool> IsPromoted(Guid articleId)
        {
            var article = await _articleRepository.GetById(articleId);
            if (article == null)
                return false;

            if (!article.IsOnSale)
                return false;

            if (article.MarginPercentage < 5)
                return false;

            return await IsInStock(articleId);
        }

        private async Task<bool> IsInStock(Guid articleId)
        {
            var qty = await _stockService.GetQuantity(articleId);

            return qty > 0;
        }
    }
}