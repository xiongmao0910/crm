namespace locy_api.Models.Response
{
    public class ResponsePage
    {
        public Boolean Status { get; set; }
        public string Message { get; set; } = "";
        public Object? Data { get; set; } = null;
        public int TotalRowCount { get; set; } = 0;
    }
}
