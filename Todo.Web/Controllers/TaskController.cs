using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Todo.Core.Database.Tables;
using Todo.Core.Domain.AppConstants;
using Todo.Service;

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
    }
}