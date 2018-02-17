using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QuickAudit.Domain
{
    //[Table("AuditEntry")]
    public partial class AuditEntry
    {
        public AuditEntry()
        {
            AuditEntryProperties = new HashSet<AuditEntryProperty>();
        }

        public int AuditEntryId { get; set; }

        [Required]
        [StringLength(255)]
        public string EntityTypeName { get; set; }

        public int State { get; set; }

        [Required]
        [StringLength(255)]
        public string StateName { get; set; }

        [Required]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<AuditEntryProperty> AuditEntryProperties { get; set; }
    }
}
