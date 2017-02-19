using System;
using System.Net;
using System.Web.Mvc;
using Todo.Core.Database.Tables;
using Todo.Core.Domain.AppConstants;
using Todo.Service;
using System.Linq;
using Todo.Web.Models;

namespace Todo.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region properties
        private TaskService _taskService;
        #endregion

        #region constructor
        public HomeController()
        {
            _taskService = _unitOfWork.TaskService;
        }
        #endregion

        public ActionResult Index()
        {
            var groups = _taskService.GetAllGroups()
                .OrderBy(x => x.DisplayOrder);

            return View(groups);
        }

        #region task operations
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

            return PartialView("_TaskPartial", task);
        }

        public ActionResult _UpdateTask(int id, bool isCompleted = false, string updateField = "", string name = "")
        {
            var task = _taskService.FindTask(id);

            if (updateField == "is_completed")
            {
                task.IsCompleted = isCompleted;
            }
            if (updateField == "name")
            {
                task.Name = name;
            }

            task.UpdateDate = DateTime.Now;
            task.UpdateUserId = 0;
            _unitOfWork.SaveChanges();

            return PartialView("_TaskPartial", task);
        }

        public ActionResult _TaskList(int groupId)
        {
            var taskModel = new TaskListModel();

            taskModel.TodoTaskList = _taskService.GetAllTasks()
                .Where(x => x.GroupId == groupId && !x.IsCompleted)
                .OrderBy(x => x.DisplayOrder)
                .ToList();
            taskModel.DoneTaskList = _taskService.GetAllTasks()
                .Where(x => x.GroupId == groupId && x.IsCompleted)
                .OrderByDescending(x => x.UpdateDate)
                .ToList();

            return PartialView("_TaskList", taskModel);
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

        #endregion

        #region group operations
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

            return PartialView("_GroupPartial", group);
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

        public ActionResult _UpdateGroup(int id, string name = "")
        {
            var group = _taskService.FindGroup(id);

            group.Name = name;
            group.UpdateDate = DateTime.Now;
            group.UpdateUserId = 0;
            _unitOfWork.SaveChanges();

            return PartialView("_GroupPartial", group);
        }

        public ActionResult _UpdateTaskGroup(int groupId, int taskId)
        {
            try
            {
                var task = _taskService.FindTask(taskId);

                task.GroupId = groupId;
                task.UpdateDate = DateTime.Now;
                task.UpdateUserId = 0;

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