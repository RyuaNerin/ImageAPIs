using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageAPIs.Search
{
	public class EshuushuuOptions : SearchOption
	{
		public EshuushuuOptions(string tags)
			: base(Ratings.All, tags, null, null)
		{ }
		public EshuushuuOptions(Ratings rating, string tags)
			: base(rating, tags, null, null)
		{ }
		public EshuushuuOptions(string tags, int? page)
			: base(Ratings.All, tags, page, null)
		{ }
		public EshuushuuOptions(Ratings rating, string tags, int? page)
			: base(rating, tags, page, null)
		{ }
		public EshuushuuOptions(Ratings rating, string tags, int? page, int? limit)
			: base(rating, tags, page, limit)
		{ }

		public EshuushuuOptions(string tags, string source, string characters, string artist)
			: base(Ratings.All, tags, null, null)
		{
			this.Source = source;
			this.Characters = characters;
			this.Artist = artist;
		}
		public EshuushuuOptions(Ratings rating, string tags, string source, string characters, string artist)
			: base(rating, tags, null, null)
		{
			this.Source = source;
			this.Characters = characters;
			this.Artist = artist;
		}
		public EshuushuuOptions(string tags, string source, string characters, string artist, int? page)
			: base(Ratings.All, tags, page, null)
		{
			this.Source = source;
			this.Characters = characters;
			this.Artist = artist;
		}
		public EshuushuuOptions(Ratings rating, string tags, string source, string characters, string artist, int? page)
			: base(rating, tags, page, null)
		{
			this.Source = source;
			this.Characters = characters;
			this.Artist = artist;
		}
		public EshuushuuOptions(Ratings rating, string tags, string source, string characters, string artist, int? page, int? limit)
			: base(rating, tags, page, limit)
		{
			this.Source = source;
			this.Characters = characters;
			this.Artist = artist;
		}

		[Obsolete]
		public new Ratings Rating { get; set; }

		[Obsolete]
		public new int? Limit { get; set; }

		public string Source { get; set; }
		public string Characters { get; set; }
		public string Artist { get; set; }
	}
}
