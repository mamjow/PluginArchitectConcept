using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginB.Db.Models
{
    public class DxMessage
    {
        public int ID { get; set; }

        public string? Message { get; set; }

        public DateTime? CreationDate { get; set; }

        public bool? Processed { get; set; }

        public DateTime? ProcessedDate { get; set; }

        public bool? ProcessedERP { get; set; }

        public DateTime? ProcessedDateERP { get; set; }

        public string? MessageType { get; set; }

        public string? SourceType { get; set; }

        public string? SourceIdentifier { get; set; }

        public string? MainEIN { get; set; }

        public string? Description { get; set; }
    }
}
