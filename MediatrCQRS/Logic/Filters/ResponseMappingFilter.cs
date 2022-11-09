using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MediatrCQRS.Logic.Filters
{
    //TODO: #12 Extras: Reponse Mapping Filter - http status code reflected in a CQRS Response
    public class ResponseMappingFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.Value is CQRSResponse cqrsResponse && cqrsResponse.StatusCode != HttpStatusCode.OK)
                context.Result = new ObjectResult(new { cqrsResponse.ErrorMessage }) { StatusCode = (int)cqrsResponse.StatusCode };
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
