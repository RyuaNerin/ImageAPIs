using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageAPIs
{
	public class SearchResult
	{
		public bool Success { get; internal set; }
		public IList<ImageInfo> Result { get; internal set; }
		public Exception Error { get; internal set; }
	}
}
