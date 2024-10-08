﻿using CleanBase.Core.Entities;
using Core.Constants;
using Core.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class CustomCanvas : EntityNameAuditActive
    {
        [Column(TypeName = "jsonb")]
        public string Content { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Images { get; set; }

        public string? CanvasCode { get; set; }

        [Required]
        public string? LensVRUrl { get; set; }

        public bool IsPublic { get; set; } = false;

        [Column(TypeName = "money")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [NotMapped]
        public ColorEnum Color { get; set; }

        [Required]
        [MaxLength(20)]
        public string ColorString
        {
            get => Color.ToString();
            set => Color = Enum.TryParse(value, out ColorEnum color) ? color : default;
        }

        public string Sizes { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        public Guid TemplateId { get; set; }

        public Guid? DefaultVoucherId { get; set; }

        [NotMapped]
        public List<SizeEnum> SizeList
        {
            get => Sizes.ParseEnumList<SizeEnum>();
            set => Sizes = value.SerializeEnumList();
        }

        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }

        [ForeignKey(nameof(TemplateId))]
        public virtual TemplateCanvas Template { get; set; }


        [ForeignKey(nameof(DefaultVoucherId))]
        public virtual Voucher? Voucher { get; set; }
    }
}
