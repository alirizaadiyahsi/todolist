using System.Collections.Generic;

namespace Todo.Core.Database.Tables
{
    public class tblPermission : BaseEntity
    {
        public tblPermission()
        {
            tblRoles = new HashSet<tblRole>();
        }

        public int PermissionNo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<tblRole> tblRoles { get; set; }
    }
}
