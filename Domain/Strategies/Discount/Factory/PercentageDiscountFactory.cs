using Core.Strategies.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Strategies.Discount.Factory
{
    public class PercentageDiscountFactory : IDiscountStrategyFactory
    {
        private readonly decimal _discount;

        public PercentageDiscountFactory( decimal discount = 0)
        {
            _discount = discount;
        }

        public string DiscountTypeName => "QuantityBased";

        public IDiscountStrategy Create()
        {
            return new PercentageDiscount(_discount);
        }
    }
}
