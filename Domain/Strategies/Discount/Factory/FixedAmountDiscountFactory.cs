using Core.Strategies.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Strategies.Discount.Factory
{
    public class FixedAmountDiscountFactory : IDiscountStrategyFactory
    {
        private readonly int _discount;

        public FixedAmountDiscountFactory(int discount = 0)
        {
            _discount = discount;
        }
        public string DiscountTypeName => "FixedAmount";

        public IDiscountStrategy Create()
        {
            return new FixedAmountDiscount(_discount);
        }
    }
}
