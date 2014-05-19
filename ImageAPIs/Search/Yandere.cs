using System;

namespace ImageAPIs.Search
{
	// http://yande.re/help/api

	public class Yandere : SearchBooru
	{
		public override int EngineID { get { return EngineIDs.eYandere; } }

		public Yandere()
			: base("http://yande.re/post.json", "tags", "page", "limit", true)
		{
		}
	}
}
