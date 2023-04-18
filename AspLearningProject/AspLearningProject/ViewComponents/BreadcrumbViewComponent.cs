using AspLearningProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspLearningProject.ViewComponents
{
    public class BreadcrumbViewComponent : ViewComponent
    {
       

        public IViewComponentResult Invoke(List<BreadcrumbElement> breadcrumbElements)
        {
            return View(breadcrumbElements);
        }
    }
}
