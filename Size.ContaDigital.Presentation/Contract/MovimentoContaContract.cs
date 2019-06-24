using Size.ContaDigital.Model;
using System.Collections.Generic;

namespace Size.ContaDigital.Presentation.Contract
{
    public class MovimentoContaContract
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object ProcessingTime { get; set; }
        public IEnumerable<MovimentoConta> Object { get; set; }
    }
}
