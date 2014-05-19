using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageAPIs
{
	public class SearchOption
	{
		public SearchOption()
			: this(Ratings.All, null, null, null)
		{ }
		public SearchOption(string tags)
			: this(Ratings.All, tags, null, null)
		{ }
		public SearchOption(Ratings rating, string tags)
			: this(rating, tags, null, null)
		{ }
		public SearchOption(string tags, int page)
			: this(Ratings.All, tags, page, null)
		{ }
		public SearchOption(Ratings rating, string tags, int? page)
			: this(rating, tags, page, null)
		{ }
		public SearchOption(Ratings rating, string tags, int? page, int? limit)
		{
			this.Rating = rating;
			this.Tags = tags;
			this.Limit = limit;
			this.Page = page;
		}

		public Ratings Rating { get; set; }
		public string Tags { get; set; }
		public int? Limit { get; set; }
		public int? Page { get; set; }
	}
}
