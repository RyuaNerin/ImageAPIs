using System;

namespace ImageAPIs.Search
{
	// http://konachan.com/help/api

	public class Konachan : SearchBooru
	{
		public override int EngineID { get { return EngineIDs.eKonachan; } }

		public Konachan()
			: base("http://konachan.com/post.json", "tags", "page", "limit", true)
		{
		}
	}
}
