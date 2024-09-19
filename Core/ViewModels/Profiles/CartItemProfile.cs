using AutoMapper;
using CleanBase.Core.Domain.Generic;
using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.ViewModels.Requests.CartItem;
using Core.ViewModels.Responses.CartItem;

namespace Core.ViewModels.Profiles
{
    public class CartItemProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<CartItemRequest, CartItem>();
            CreateMap<CartItem, CartItemResponse>()
                .ForMember(dest => dest.CustomCanvas, opt => opt.MapFrom(src => src.CustomCanvas));
            CreateMap<ListResult<CartItem>, ListResult<CartItemResponse>>();
        }
    }
}