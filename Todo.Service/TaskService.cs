using System;
using System.Data.Entity;
using System.Linq;
using Todo.Core.Database.Tables;
using Todo.Data;

namespace Todo.Service
{
    public class TaskService : BaseService
    {
        public TaskService(TodoContext context)
        {
            _context = context;
        }

        #region tasks
        public void AddTask(tblTask task)
        {
            _context.tblTasks.Add(task);
        }

        public IQueryable<tblTask> GetAllTasks()
        {
            var tasks = _context.tblTasks;

            return tasks;
        }
        #endregion

        #region groups
        public IQueryable<tblGroup> GetAllGroups()
        {
            var groups = _context.tblGroups;

            return groups;
        }

        public void AddGroup(tblGroup group)
        {
            _context.tblGroups.Add(group);
        }

        public tblGroup FindGroup(int groupId)
        {
            var group = _context.tblGroups.Find(groupId);

            return group;
        }

        public void DeleteGroup(tblGroup groupToDelete)
        {
            _context.tblGroups.Attach(groupToDelete);
            _context.Entry(groupToDelete).State = EntityState.Deleted;
        }
        #endregion
    }
}
