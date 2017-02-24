using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Database.Tables;
using Todo.Data;

namespace Todo.Service.MembershipService
{
    public class PermissionService : BaseService
    {
        public PermissionService(TodoContext context)
        {
            _context = context;
        }

        public IQueryable<tblPermission> GetPermissionsByRole(int roleId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<tblPermission> GetAllPermissions()
        {
            throw new NotImplementedException();
        }

        public void InsertPermission(tblPermission permission)
        {
            throw new NotImplementedException();
        }

        public tblPermission Find(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePermission(tblPermission permission)
        {
            throw new NotImplementedException();
        }

        public void DeletePermission(tblPermission permission)
        {
            throw new NotImplementedException();
        }
    }
}
