using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Size.ContaDigital.Api.ViewModels;

namespace Size.ContaDigital.Api.Controllers
{
    public class ControllerJsonBase : ControllerBase
    {
        private Stopwatch _stopWatch;
        private JsonViewModel _jsonResult;

        public Stopwatch StopWatch
        {
            get
            {
                return _stopWatch;
            }
            set
            {
                _stopWatch = value;
            }
        }
        public JsonViewModel JsonResult
        {
            get
            {
                return _jsonResult;
            }
            set
            {
                _jsonResult = value;
            }
        }

        public ControllerJsonBase()
        {
            _stopWatch = new Stopwatch();
            _jsonResult = new JsonViewModel();
        }
    }
}
