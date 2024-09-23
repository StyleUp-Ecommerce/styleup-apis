using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Strategies.Discount
{
    public interface IDiscountStrategyFactory
    {
        string DiscountTypeName { get; }
        IDiscountStrategy Create();
    }
}
