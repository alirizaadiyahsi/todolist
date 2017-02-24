using System;
using Todo.Data;
using Todo.Service.MembershipService;

namespace Todo.Service
{
    public class UnitOfWork : IDisposable
    {
        private TodoContext _context = new TodoContext();
        private TaskService _taskService;
        private UserService _userService;
        private RoleService _roleService;
        private PermissionService _permissionService;

        public TaskService TaskService
        {
            get
            {
                if (_taskService == null)
                {
                    _taskService = new TaskService(_context);
                }
                return _taskService;
            }
        }

        public UserService UserService
        {
            get
            {
                if (_userService == null)
                {
                    _userService = new UserService(_context);
                }
                return _userService;
            }
        }

        public RoleService RoleService
        {
            get
            {
                if (_roleService == null)
                {
                    _roleService = new RoleService(_context);
                }
                return _roleService;
            }
        }

        public PermissionService PermissionService
        {
            get
            {
                if (_permissionService == null)
                {
                    _permissionService = new PermissionService(_context);
                }
                return _permissionService;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
