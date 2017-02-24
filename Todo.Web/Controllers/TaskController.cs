using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Todo.Core.Database.Tables;
using Todo.Core.Domain.AppConstants;
using Todo.Service;
using Todo.Web.Application.Membership;
using Todo.Web.Models;

namespace Todo.Web.Controllers
{
    public class TaskController : AuthorizedController
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

        #region task operations
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
                InsertUserId = CurrentUser.Id,
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

        public ActionResult _UpdateTask(int taskId, string name)
        {
            var task = _taskService.FindTask(taskId);

            task.Name = name;
            task.UpdateDate = DateTime.Now;
            task.UpdateUserId = 0;

            _unitOfWork.SaveChanges();

            return PartialView("_Task", task);
        }

        public ActionResult _UpdateTaskStatus(int taskId, bool isCompleted)
        {
            var task = _taskService.FindTask(taskId);

            task.IsCompleted = isCompleted;
            task.UpdateDate = DateTime.Now;
            task.UpdateUserId = 0;

            _unitOfWork.SaveChanges();

            return PartialView("_Task", task);
        }

        public ActionResult _UpdateTasksOrder(int[] taskIds)
        {
            try
            {
                for (int i = 0; i < taskIds.Count(); i++)
                {
                    var task = _taskService.FindTask(taskIds[i]);

                    task.DisplayOrder = i;
                    task.UpdateDate = DateTime.Now;
                    task.UpdateUserId = 0;
                }

                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                var responseModel = CreateResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message, ResponseStatusTypes.Danger);

                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult _DeleteTask(int taskId)
        {
            try
            {
                var task = _taskService.FindTask(taskId);

                _taskService.DeleteTask(task);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                var responseModel = CreateResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message, ResponseStatusTypes.Danger);

                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion




        #region group operations
        public ActionResult _GroupList()
        {
            var groups = _taskService.GetAllGroups()
                .Where(x => x.UserId == CurrentUser.Id)
                .OrderBy(x => x.DisplayOrder);

            return PartialView(groups);
        }

        [HttpPost]
        public ActionResult _AddGroup(string groupName)
        {
            var lastGroup = _taskService.GetAllGroups()
                .Where(x => x.UserId == CurrentUser.Id)
                .OrderByDescending(x => x.DisplayOrder)
                .FirstOrDefault() ?? new tblGroup();

            var group = new tblGroup
            {
                DisplayOrder = lastGroup.DisplayOrder + 1,
                InsertDate = DateTime.Now,
                InsertUserId = CurrentUser.Id,
                UserId = CurrentUser.Id,
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

        public ActionResult _UpdateGroup(int groupId, string name)
        {
            var group = _taskService.FindGroup(groupId);

            group.Name = name;
            group.UpdateDate = DateTime.Now;
            group.UpdateUserId = 0;

            _unitOfWork.SaveChanges();

            return PartialView("_Group", group);
        }

        public ActionResult _UpdateGroupsOrder(int[] groupIds)
        {
            try
            {
                for (int i = 0; i < groupIds.Count(); i++)
                {
                    var group = _taskService.FindGroup(groupIds[i]);

                    group.DisplayOrder = i;
                    group.UpdateDate = DateTime.Now;
                    group.UpdateUserId = 0;
                }

                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                var responseModel = CreateResponse(HttpStatusCode.InternalServerError, ex.GetBaseException().Message, ResponseStatusTypes.Danger);

                return Json(responseModel, JsonRequestBehavior.AllowGet);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

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
        #endregion
    }
}