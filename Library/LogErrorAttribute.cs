using System;
using System.Web.Mvc;
using System.Web.Routing;


namespace Library
{
    public class LogErrorAttribute : ActionFilterAttribute, IExceptionFilter
    {
        private String _controller = "Error";
        private String _action = "Index";

        
        public LogErrorAttribute()
        {
        }


        public LogErrorAttribute(String controller, String action)
        {
            _controller = controller;
            _action = action;
        }


        void IExceptionFilter.OnException(ExceptionContext ec)
        {
            //ec.Result = new RedirectToRouteResult(new RouteValueDictionary
            //{
            //    {"controller", _controller},
            //    {"action", _action}
            //});

            //ec.ExceptionHandled = true;
        }
    }
}