using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.Core.Database.Tables
{
    public class tblGroup : BaseEntity
    {
        public tblGroup()
        {
            tblTasks = new HashSet<tblTask>();
        }

        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual tblUser tblUser { get; set; }

        public virtual ICollection<tblTask> tblTasks { get; set; }
    }
}
