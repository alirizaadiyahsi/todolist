using Todo.Core.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Data;

namespace Todo.Service.MembershipService
{
    public class RoleService : BaseService
    {
        public RoleService(TodoContext context)
        {
            _context = context;
        }

        public bool IsUserInRole(string userName, string roleName)
        {
            return _context.tblRoles
                 .FirstOrDefault(x => x.Name == roleName)
                 .tblUsers
                 .Any(x => x.Name == userName);
        }

        public IEnumerable<tblRole> GetRolesByUserName(string username)
        {
            return _context.tblUsers
                .FirstOrDefault()
                .tblRoles;
        }

        public IQueryable<tblRole> GetRolesByUserId(int ıd)
        {
            throw new NotImplementedException();
        }

        public IQueryable<tblRole> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public void InsertRole(tblRole role)
        {
            throw new NotImplementedException();
        }

        public tblRole Find(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRole(tblRole role)
        {
            throw new NotImplementedException();
        }
    }
}
