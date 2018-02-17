using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QuickAudit.Domain
{
    [Table("AuditEntryProperty")]
    public partial class AuditEntryProperty
    {
        public int AuditEntryPropertyId { get; set; }

        public int AuditEntryId { get; set; }

        [StringLength(255)]
        public string PropertyName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public virtual AuditEntry AuditEntry { get; set; }
    }
}
