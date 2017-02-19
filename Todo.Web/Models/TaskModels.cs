using System.Collections.Generic;
using Todo.Core.Database.Tables;

namespace Todo.Web.Models
{
    public class TaskListModel
    {
        public List<tblTask> TodoTaskList { get; set; }
        public List<tblTask> DoneTaskList { get; set; }
    }
}