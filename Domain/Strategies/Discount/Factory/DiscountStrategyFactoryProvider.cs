using CleanBase.Core.Domain.Exceptions;
using Core.Strategies.Discount;
using System.Collections.Generic;

namespace Domain.Strategies.Discount.Factory
{
    public class DiscountStrategyFactoryProvider
    {
        private readonly Dictionary<string, IDiscountStrategyFactory> _factories;

        public DiscountStrategyFactoryProvider(IEnumerable<IDiscountStrategyFactory> factories)
        {
            _factories = new Dictionary<string, IDiscountStrategyFactory>();

            foreach (var factory in factories)
            {
                _factories[factory.DiscountTypeName] = factory;
            }
        }

        public IDiscountStrategyFactory GetFactory(string discountTypeName)
        {
            if (_factories.TryGetValue(discountTypeName, out var factory))
            {
                return factory;
            }
            throw new DomainException($"Discount strategy '{discountTypeName}' is not valid!");
        }
    }
}
