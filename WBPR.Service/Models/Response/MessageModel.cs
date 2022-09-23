namespace WBPR.Service.Models.Response
{
    public class MessageModel<T>
    {
        public bool Success { get; set; } = true;

        public string Msg { get; set; } = "";

        public T Data { get; set; }
    }
}
