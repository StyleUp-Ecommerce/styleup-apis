using CleanBase.Core.Domain.Generic;
using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.ViewModels.Requests.Cart;
using Core.ViewModels.Responses.Cart;

namespace Core.ViewModels.Profiles
{
    public class CartProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<CartRequest, Cart>();
            CreateMap<Cart, CartResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems));
            CreateMap<ListResult<Cart>, ListResult<CartResponse>>();

        }
    }
}