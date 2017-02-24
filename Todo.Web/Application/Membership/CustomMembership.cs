using System;
using FormsAuthenticationExtensions;
using Todo.Core.Domain.AppConstants;
using System.Web.Security;
using System.Web;

namespace Todo.Web.Application.Membership
{
    public static class CustomMembership
    {
        /// <summary>
        /// Get current logged in user
        /// </summary>
        /// <returns>Logged in user</returns>
        public static CurrentUser CurrentUser()
        {
            var currentUser = HttpContext.Current.User;
            var ticketData = ((FormsIdentity)currentUser.Identity).Ticket.GetStructuredUserData();
            var user = new CurrentUser();

            user.Id = Int32.Parse(ticketData[MembershipFields.Id]);
            user.Name = currentUser.Identity.Name;
            user.Email = ticketData[MembershipFields.Email];

            return user;
        }
    }

    public class CurrentUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
