﻿using System;
using System.Net;
using System.Web.Mvc;
using Todo.Core.Database.Tables;
using Todo.Core.Domain.AppConstants;
using Todo.Service;
using System.Linq;

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
                .OrderByDescending(x => x.DisplayOrder);

            return View(groups);
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

            return PartialView("_TaskPartial", task);
        }

        public ActionResult _TaskList(int groupId)
        {
            var tasks = _taskService.GetAllTasks()
                .Where(x => x.GroupId == groupId)
               .OrderByDescending(x => x.DisplayOrder);

            return PartialView("_TaskList", tasks);
        }

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
    }
}