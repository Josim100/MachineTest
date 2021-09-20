using System;
using System.IO;
using System.Web.Mvc;

namespace MachineTest.Controllers
{

    [HandleError]
    public abstract class BaseController : Controller
    {
        protected string BaseUrl { get; private set; }
        protected string PublicUrl { get; private set; }

        public BaseController()
        {
        }

        [HandleError]
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            this.SaveLog(filterContext.Exception);
        }

        [HandleError]
        private void SaveLog(Exception e)
        {
            using (StreamWriter sw = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath("~/logs/error/Exceptionlog-" + String.Format("{0:yyyy-MM-dd}", DateTime.Today) + ".txt")))
            {
                sw.WriteLine("--------- " + Convert.ToString(DateTime.Now) + " --------- --------- --------- --------- ---------");
                if (e != null)
                {
                    sw.WriteLine("Message: " + e.Message);
                    sw.WriteLine("Stack Trace: ");
                    sw.WriteLine(e.StackTrace);
                    sw.WriteLine("");
                }
                sw.Close();
            }
        }
    }   
}