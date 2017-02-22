using System.Collections.Generic;
using Todo.Core.Database.Tables;

namespace Todo.Web.Models
{
    public class TaskListModel
    {
        public List<tblTask> TaskListWaiting { get; set; }
        public List<tblTask> TaskListDone { get; set; }
    }
}