﻿using CleanBase.Core.ViewModels.Response.Base;

namespace Core.ViewModels.Responses.CustomCanvas
{
    public class CustomCanvasResponse : EntityAuditNameResponseBase
    {
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string? LensVRUrl { get; set; }
        public decimal Price { get; set; }
        public string ColorString { get; set; }
        public Guid AuthorId { get; set; }
        public Guid TemplateId { get; set; }
    }
}
