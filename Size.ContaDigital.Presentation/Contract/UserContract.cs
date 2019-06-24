using Size.ContaDigital.Model;

namespace Size.ContaDigital.Presentation.Contract
{
    public class UserContract
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object ProcessingTime { get; set; }
        public User Object { get; set; }
    }
}
