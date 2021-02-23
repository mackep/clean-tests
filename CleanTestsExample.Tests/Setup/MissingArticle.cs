using System;
using AutoFixture;
using NSubstitute;

namespace CleanTestsExample.Tests.Setup
{
    public abstract class MissingArticle : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Freeze<IArticleRepository>().GetById(Arg.Any<Guid>()).Returns((Article) null);
        }
    }
}