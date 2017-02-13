namespace Todo.Core.Domian.Models
{
    public class AjaxResult
    {
        public ResponseStatus ResponseStatus { get; set; }
        public string ResponseMessage { get; set; }
        public object Data { get; set; }
    }

    public class ResponseStatus
    {
        public string StatusName { get; set; }
        public string StatusKey { get; set; }
        public int StatusCode { get; set; }
        public string NotificationTemplateName { get; set; }
    }
}
