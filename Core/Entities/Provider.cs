using CleanBase.Core.Entities;
using Core.Constants;
using Core.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Provider : EntityNameAuditActive
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "money")]
        public decimal MinimumPrice { get; set; }

        public string Colors { get; set; }
        public string Sizes { get; set; }
        public string Strengths { get; set; }

        [NotMapped]
        public List<ColorEnum> ColorList
        {
            get => Colors.ParseEnumList<ColorEnum>();
            set => Colors = value.SerializeEnumList();
        }

        [NotMapped]
        public List<SizeEnum> SizeList
        {
            get => Sizes.ParseEnumList<SizeEnum>();
            set => Sizes = value.SerializeEnumList();
        }

        [NotMapped]
        public List<StrengthEnum> StrengthList
        {
            get => Strengths.ParseEnumList<StrengthEnum>();
            set => Strengths = value.SerializeEnumList();
        }

        public virtual ICollection<ProviderRate>? ProviderRates { get; set; }
        public virtual ICollection<TemplateCanvas>? TemplateCanvas { get; set; }

    }

}
