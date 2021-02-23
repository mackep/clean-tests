using System;
using AutoFixture;
using NSubstitute;

namespace CleanTestsExample.Tests.Setup
{
    public class KnownArticle : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Freeze<IArticleRepository>().GetById(Arg.Any<Guid>()).Returns(info => fixture.Freeze<Article>());
        }
    }
}