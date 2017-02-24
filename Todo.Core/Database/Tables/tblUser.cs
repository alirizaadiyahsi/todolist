using System.Collections.Generic;

namespace Todo.Core.Database.Tables
{
    public class tblUser : BaseEntity
    {
        public tblUser()
        {
            tblRoles = new HashSet<tblRole>();
            tblGroups = new HashSet<tblGroup>();
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public virtual ICollection<tblRole> tblRoles { get; set; }
        public virtual ICollection<tblGroup> tblGroups { get; set; }
    }
}
