using Core.Strategies.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Strategies.Discount
{
    public class PercentageDiscount : IDiscountStrategy
    {
        private readonly decimal _discountValue;

        public PercentageDiscount(decimal discountValue)
        {
            _discountValue = discountValue;
        }

        public decimal ApplyDiscount(decimal totalAmount)
        {
            return totalAmount - (totalAmount * _discountValue / 100);
        }
    }

}