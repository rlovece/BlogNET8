using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogNET8.Models
{
	public class PaginatedList<T> : List<T>
	{
		public int PageIndex { get; set; }
		public int PageCount { get; private set; }
		
		public string SearchString { get; private set; }
		public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, string searchString) 
		{
			PageIndex = pageIndex;
			PageCount = (int)Math.Ceiling(count / (double)pageSize);
			SearchString = searchString;

			AddRange(items);
		}

		public bool HasPreviusPage => PageIndex > 0;

		public bool HasNextiusPage => PageIndex < PageCount;
	}
}
