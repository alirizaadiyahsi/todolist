using FormsAuthenticationExtensions;
using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Security;
using Todo.Core.Database.Tables;
using Todo.Core.Domain.AppConstants;
using Todo.Service.MembershipService;
using Todo.Web.Models;

namespace Todo.Web.Controllers
{
    public class AccountController : PublicController
    {
        private UserService _userService;

        public AccountController()
        {
            _userService = _unitOfWork.UserService;
        }

        #region account operations
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model, string ReturnUrl)
        {
            var user = _userService.FindUserByName(model.Name);

            if (user != null && ModelState.IsValid)
            {
                var ticketData = new NameValueCollection
                    {
                        { StringKeys.Membership_Name, user.Name},
                        { StringKeys.Membership_Id, user.Id.ToString() },
                        { StringKeys.Membership_Email, user.Email}
                    };

                // create a auth cookie with ticket data
                new FormsAuthentication().SetAuthCookie(model.Name, model.Rememberme, ticketData);

                return RedirectToLocal(ReturnUrl);
            }
            else
            {
                ModelState.AddModelError("", "User name or password fields wrong!");
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new tblUser();

                    user.Password = model.Password;
                    user.Name = model.Name;
                    user.InsertUserId = 0;
                    user.InsertDate = DateTime.Now;
                    user.Email = model.Email;

                    _userService.InsertUser(user);
                    _unitOfWork.SaveChanges();

                    return RedirectToAction("RegisterSuccess", "Account");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Register is unsuccessful. Error: " + ex.Message);
                }
            }

            return View(model);
        }

        public ActionResult RegisterSuccess()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var user = _userService.FindUserByName(User.Identity.Name);

            if (ModelState.IsValid && user != null)
            {
                user.Password = model.NewPassword;
                _userService.UpdateUser(user);

                _unitOfWork.SaveChanges();

                ViewBag.SuccessDesc = "Your password has been changed, successfully!";
                model = new ChangePasswordModel();
            }
            else
            {
                ModelState.AddModelError("", "Old password is invalid!");
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// UserName remote control
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// <returns>True if user NOT exist</returns>
        [AllowAnonymous]
        public JsonResult ValidateName(string Name)
        {
            var result = _userService.ValidateName(Name);

            // if user exist
            if (result)
            {
                return Json("User name that you enter is not exist!", JsonRequestBehavior.AllowGet);
            }

            // return true if user NOT exist
            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        // RegisterModel içerisindeki Email alanını
        // RemoteAttribute ile kontrol eder
        public JsonResult ValidateEmail(string Email)
        {
            var result = _userService.ValidateEmail(Email);

            if (result)
            {
                return Json("Girdiğiniz e-posta adresi sistemde zaten mevcut!", JsonRequestBehavior.AllowGet);
            }

            return Json(!result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region private methods - redirect to local url
        [AllowAnonymous]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}