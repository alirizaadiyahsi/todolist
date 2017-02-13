using System.Collections.Generic;

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

        public virtual ICollection<tblTask> tblTasks { get; set; }
    }
}
