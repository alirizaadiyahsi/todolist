using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Core.Database.Tables
{
    public class tblTask : BaseEntity
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsCompleted { get; set; }

        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual tblGroup Group { get; set; }
    }
}
