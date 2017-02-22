using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Todo.Core.Database.Tables;
using Todo.Core.Domain.AppConstants;
using Todo.Service;
using Todo.Web.Models;

namespace Todo.Web.Controllers
{
    public class TaskController : BaseController
    {
        private readonly TaskService _taskService;

        public TaskController()
        {
            _taskService = _unitOfWork.TaskService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _TaskList(int groupId)
        {
            var taskModel = new TaskListModel();

            taskModel.TaskListWaiting = _taskService.GetAllTasks()
                .Where(x => x.GroupId == groupId && !x.IsCompleted)
                .OrderBy(x => x.DisplayOrder)
                .ToList();
            taskModel.TaskListDone = _taskService.GetAllTasks()
                .Where(x => x.GroupId == groupId && x.IsCompleted)
                .OrderByDescending(x => x.UpdateDate)
                .Take(20)
                .ToList();

            return PartialView(taskModel);
        }

        public ActionResult _AddTask(string taskName, int groupId = 0)
        {
            var lastTask = _taskService.GetAllTasks()
                .OrderByDescending(x => x.DisplayOrder)
                .FirstOrDefault() ?? new tblTask();

            var task = new tblTask
            {
                DisplayOrder = lastTask.DisplayOrder + 1,
                GroupId = groupId,
                InsertDate = DateTime.Now,
                InsertUserId = 0,
                IsCompleted = false,
                Name = taskName
            };

            try
            {
                _taskService.AddTask(task);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                var responseModel = CreateResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message, ResponseStatusTypes.Danger);

                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }

            return PartialView("_Task", task);
        }

        public ActionResult _GroupList()
        {
            var groups = _taskService.GetAllGroups();

            return PartialView(groups);
        }

        [HttpPost]
        public ActionResult _AddGroup(string groupName)
        {
            var lastGroup = _taskService.GetAllGroups()
                .OrderByDescending(x => x.DisplayOrder)
                .FirstOrDefault() ?? new tblGroup();

            var group = new tblGroup
            {
                DisplayOrder = lastGroup.DisplayOrder + 1,
                InsertDate = DateTime.Now,
                InsertUserId = 0,
                Name = groupName
            };

            try
            {
                _taskService.AddGroup(group);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                var responseModel = CreateResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message, ResponseStatusTypes.Danger);

                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }

            return PartialView("_Group", group);
        }

        // TODO: Update group

        public ActionResult _DeleteGroup(int groupId)
        {
            try
            {
                var groupToDelete = _taskService.FindGroup(groupId);

                _taskService.DeleteGroup(groupToDelete);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                var responseModel = CreateResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message, ResponseStatusTypes.Danger);

                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}