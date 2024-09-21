using CleanBase.Core.Entities;

namespace Core.Entities
{
    public class Pet : EntityNameAuditActive
    {
        public int Age { get; set; }
    }
}
