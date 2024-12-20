﻿using CleanBase.Core.Entities;
using Core.ViewModels.Responses.OrderItem;
using Core.ViewModels.Responses.Transaction;
using Newtonsoft.Json;

namespace Core.ViewModels.Responses.Order
{
    public class OrderResponse : EntityAuditActive
    {
        public string OrderStatus { get; set; }
        public string OrderCode { get; set; }
        public List<OrderItemDetailResponse> Items { get; set; }
        public GetTransactionWithOrderCodeResponse Transaction { get; set; }
        public decimal TotalPrice { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public string Address { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientMail { get; set; }
        public string RecipientName { get; set; }


    }
}
