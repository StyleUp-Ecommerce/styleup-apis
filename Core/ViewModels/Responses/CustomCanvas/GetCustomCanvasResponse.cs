﻿using CleanBase.Core.ViewModels.Response.Base;

namespace Core.ViewModels.Responses.CustomCanvas
{
    public class GetCustomCanvasResponse : EntityAuditNameResponseBase
    {
        public string Content { get; set; }
        public List<string> Images { get; set; }
        public string? LensVRUrl { get; set; }
        public bool IsPublic { get; set; } = false;
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Sizes { get; set; }
        public Guid AuthorId { get; set; }
        public Guid TemplateId { get; set; }

    }
}
