using System.Net;
using System.Web.Mvc;
using Todo.Core.Domain.AppConstants;
using Todo.Core.Domian.Models;
using Todo.Service;
using Todo.Web.Application.Membership;

namespace Todo.Web.Controllers
{
    public class BaseController : Controller
    {
        protected UnitOfWork _unitOfWork = new UnitOfWork();

        protected AjaxResult CreateResponse(HttpStatusCode httpResponseCode, string message, string responseType)
        {
            Response.StatusCode = (int)httpResponseCode;

            var responseModel = new AjaxResult
            {
                ResponseMessage = message,
                ResponseStatus = AppConstantLists.ResponseStatusList[responseType]
            };

            return responseModel;
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }

    [Authorize]
    public class AuthorizedController : BaseController
    {
    }

    public class PublicController : BaseController
    { }
}