using System.Collections.Generic;
using Todo.Core.Domian.Models;

namespace Todo.Core.Domain.AppConstants
{
    /// <summary>
    /// Static class to store application constant values.
    /// </summary>
    public static class AppConstantLists
    {
        public static Dictionary<string, ResponseStatus> ResponseStatusList = new Dictionary<string, ResponseStatus> {
            {ResponseStatusTypes.Success,
                new ResponseStatus{
                    NotificationTemplateName =ResponseNotificationTemplates.Bg_Success,
                    StatusCode = (int)ResponseStatusCodes.Success,
                    StatusKey = ResponseStatusTypes.Success,
                    StatusName = "" } },
            {ResponseStatusTypes.Info,
                new ResponseStatus{
                    NotificationTemplateName =ResponseNotificationTemplates.Bg_Info,
                    StatusCode = (int)ResponseStatusCodes.Info,
                    StatusKey = ResponseStatusTypes.Info,
                    StatusName = "" } },
            {ResponseStatusTypes.Warning,
                new ResponseStatus{NotificationTemplateName=ResponseNotificationTemplates.Bg_Warning,
                    StatusCode = (int)ResponseStatusCodes.Warning,
                    StatusKey = ResponseStatusTypes.Warning,
                    StatusName = "" } },
            {ResponseStatusTypes.Danger,
                new ResponseStatus{NotificationTemplateName=ResponseNotificationTemplates.Bg_Danger,
                    StatusCode = (int)ResponseStatusCodes.Danger,
                    StatusKey = ResponseStatusTypes.Danger,
                    StatusName = "" } }
        };
    }
}
