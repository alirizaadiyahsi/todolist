namespace Todo.Core.Domain.AppConstants
{
    /// <summary>
    /// Static class to store application constant values.
    /// </summary>
    public static class ResponseStatusTypes
    {
        public readonly static string Success = "success";
        public readonly static string Info = "info";
        public readonly static string Warning = "warning";
        public readonly static string Danger = "danger";
    }

    /// <summary>
    /// Static class to store application constant values.
    /// </summary>
    public static class ResponseNotificationTemplates
    {
        public readonly static string Bg_Success = "alert-success";
        public readonly static string Bg_Info = "alert-info";
        public readonly static string Bg_Warning = "alert-warning";
        public readonly static string Bg_Danger = "alert-danger";
    }

    public static class MembershipFields
    {
        public readonly static string Id = "id";
        public readonly static string Name = "name";
        public readonly static string Email = "email";
    }

    public static class StringKeys
    {
        public readonly static string Membership_Id = "id";
        public readonly static string Membership_Name = "name";
        public readonly static string Membership_Email = "email";
    }
}
