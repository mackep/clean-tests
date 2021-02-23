using System;
using System.Collections.Generic;
using System.Reflection;
using AutoFixture.Kernel;

namespace CleanTestsExample.Tests.Setup
{
    public abstract class PropertySetup<T> : ISpecimenBuilder
    {
        private readonly Random _random;

        protected PropertySetup()
        {
            _random = new Random();
        }

        public abstract string PropertyName { get; }

        /*
         * This property can be overriden in a subclass
         * in order to provide a discrete set of possible
         * values. If not overridden, AutoFixture will be
         * used to create an instance of T.
         */
        public virtual IList<T> Values { get; } = null;

        public object Create(object request, ISpecimenContext context)
        {
            var param = request as ParameterInfo;
            var property = request as PropertyInfo;
            var isParam = param != null &&
                                            param.ParameterType == typeof(T) &&
                                            param.Name!.Equals(PropertyName,
                                                StringComparison.OrdinalIgnoreCase);

            var isProp = property != null &&
                                           property.PropertyType == typeof(T) &&
                                           property.Name!.Equals(PropertyName,
                                               StringComparison.OrdinalIgnoreCase);

            if (!isParam && !isProp) return new NoSpecimen();

            return Values == null
                ? context.Resolve(typeof(T))
                : Values[_random.Next(0, Values.Count)];
        }
    }
}