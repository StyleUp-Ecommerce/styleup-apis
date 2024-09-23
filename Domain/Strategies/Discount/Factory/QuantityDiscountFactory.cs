using Core.Strategies.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Strategies.Discount.Factory
{
    public class QuantityDiscountFactory : IDiscountStrategyFactory
    {
        private readonly int _quantity;
        private readonly decimal _discount;

        public QuantityDiscountFactory(int quantity =0, decimal discount =0)
        {
            _quantity = quantity;
            _discount = discount;
        }

        public string DiscountTypeName => "QuantityBased";

        public IDiscountStrategy Create()
        {
            return new QuantityDiscount(_discount,_quantity);
        }
    }
}
