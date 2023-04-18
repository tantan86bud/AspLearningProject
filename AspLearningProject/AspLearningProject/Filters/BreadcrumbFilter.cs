using AspLearningProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace AspLearningProject.Filters
{
    
    public class BreadcrumbAttribute : ActionFilterAttribute, IActionFilter
    {
        BreadcrumbElement breadcrumbElement = new BreadcrumbElement();
        public BreadcrumbAttribute(string controllerFrom, string actionFrom, bool isEnd = false, string Title = null)
        {
            breadcrumbElement.ControllerFrom = controllerFrom;
            breadcrumbElement.ActionFrom = actionFrom;
            breadcrumbElement.IsEnd = isEnd;
            breadcrumbElement.Title = Title;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Controller controller = context.Controller as Controller;
            if (string.IsNullOrEmpty(breadcrumbElement.Title))
            {
                if (controller.ViewBag.Title != null)
                {
                    breadcrumbElement.Title = controller.ViewBag.Title;
                }
                else
                {
                    breadcrumbElement.Title = breadcrumbElement.ControllerFrom;
                }
            }
            if (controller.ViewBag.Breadcrumbs == null)
            {
                controller.ViewBag.Breadcrumbs = new List<BreadcrumbElement>();
            }
            (controller.ViewBag.Breadcrumbs as List<BreadcrumbElement>).Add(breadcrumbElement);

        }
    }

}