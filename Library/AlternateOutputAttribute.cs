using System;
using System.Text;
using System.Web.Mvc;

namespace Library
{
    public enum RequestSourceRestriction
    {
        AllowRemoteRequests,
        DenyRemoteRequests
    }

    
    public class AlternateOutputAttribute : ActionFilterAttribute, IActionFilter
    {
        private RequestSourceRestriction _requestSourceRestriction = RequestSourceRestriction.DenyRemoteRequests;
        private JsonRequestBehavior _jsonRequestBehavior = JsonRequestBehavior.DenyGet;
        

        public AlternateOutputAttribute()
        {
        }


        public AlternateOutputAttribute(RequestSourceRestriction restriction)
        {
            _requestSourceRestriction = restriction;
        }


        public AlternateOutputAttribute(JsonRequestBehavior behavior)
        {
            _jsonRequestBehavior = behavior;
        }


        public AlternateOutputAttribute(RequestSourceRestriction restriction, JsonRequestBehavior behavior)
        {
            _requestSourceRestriction = restriction;
            _jsonRequestBehavior = behavior;
        }


        void IActionFilter.OnActionExecuted(ActionExecutedContext aec)
        {
            var vr = aec.Result as ViewResult;
            var aof = aec.RouteData.Values["alternateOutputFormat"] as String;


            if (_requestSourceRestriction == RequestSourceRestriction.DenyRemoteRequests && !aec.RequestContext.HttpContext.Request.IsLocal)
            {
                aec.Result = new EmptyResult();
            }
            else
            {
                if (vr != null) switch (aof)
                {
                    case "json": aec.Result = new JsonResult
                    {
                        JsonRequestBehavior = _jsonRequestBehavior,
                        ContentType = "application/json",
                        ContentEncoding = Encoding.UTF8,
                        Data = vr.ViewData.Model
                    };
                    break;

                    case "txt": aec.Result = new ContentResult
                    {
                        Content = "Not yet implemented",
                        ContentType = "text/plain",
                        ContentEncoding = Encoding.UTF8,
                    };
                    break;
                }
            }
        }
    }
}