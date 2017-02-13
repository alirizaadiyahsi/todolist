using System;
using Todo.Data;

namespace Todo.Service
{
    public class UnitOfWork : IDisposable
    {
        private TodoContext _context = new TodoContext();
        private TaskService _taskService;

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
