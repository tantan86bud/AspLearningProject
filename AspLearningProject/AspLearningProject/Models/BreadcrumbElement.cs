using System;

namespace AspLearningProject.Models
{
	public class BreadcrumbElement
	{
		public string ControllerFrom { get; set; }
		public string ActionFrom { get; set; }
        public string Title { get; set; }
        public bool IsEnd { get; set; }
    }

}