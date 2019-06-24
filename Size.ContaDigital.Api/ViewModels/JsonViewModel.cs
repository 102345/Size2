namespace Size.ContaDigital.Api.ViewModels
{
    public class JsonViewModel
    {
        public bool Status { get; set; }
        public string Message { get; set; } = "";
        public string ProcessingTime { get; set; }
        public object Object { get; set; }
        public JsonViewModel() { }
    }
}
