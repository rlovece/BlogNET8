using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogNET8.Models.ViewModels
{
	public class HomeVM
	{
		public IEnumerable<Slider> Sliders {  get; set; }
		public IEnumerable<Article> Articles { get; set; }

		public int PageIndex { get; set; }

		public int PageCount { get; set; }

    }
}
