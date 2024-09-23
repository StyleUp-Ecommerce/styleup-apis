using Core.Strategies.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Strategies.Discount
{
    public class QuantityDiscount : IDiscountStrategy
    {
        private readonly decimal _discountValue;
        private readonly int _quantityThreshold;

        public QuantityDiscount(decimal discountValue, int quantityThreshold)
        {
            _discountValue = discountValue;
            _quantityThreshold = quantityThreshold;
        }

        public decimal ApplyDiscount(decimal totalAmount)
        {
            return totalAmount >= _quantityThreshold ? totalAmount - _discountValue : totalAmount;
        }
    }
}
