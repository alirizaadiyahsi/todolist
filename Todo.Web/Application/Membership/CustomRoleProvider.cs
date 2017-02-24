using Todo.Service.MembershipService;
using System.Linq;
using System.Web.Security;
using Todo.Service;

namespace Todo.Web.Application.Membership
{
    public class CustomRoleProvider : RoleProvider
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        private RoleService _roleService;

        public CustomRoleProvider()
        {
            _roleService = _unitOfWork.RoleService;
        }

        public override bool IsUserInRole(string userName, string roleName)
        {
            return _roleService.IsUserInRole(userName, roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return _roleService.GetRolesByUserName(username)
                .Select(x => x.Name)
                .ToArray();
        }

        #region not implemented
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
