using System.Collections.Generic;

namespace Todo.Core.Database.Tables
{
    public class tblRole : BaseEntity
    {
        public tblRole()
        {
            tblPermissions = new HashSet<tblPermission>();
            tblUsers = new HashSet<tblUser>();
        }

        public string Name { get; set; }

        public virtual ICollection<tblPermission> tblPermissions { get; set; }
        public virtual ICollection<tblUser> tblUsers { get; set; }
    }
}
