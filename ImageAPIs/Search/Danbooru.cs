using System;

namespace ImageAPIs.Search
{
	// http://danbooru.donmai.us/wiki_pages/43568
	
	public class Danbooru : SearchBooru
	{
		public override int EngineID { get { return EngineIDs.eDanbooru; } }

		public Danbooru()
			: base("http://danbooru.donmai.us/posts.json", "tags", "page", "limit", true)
		{
		}
	}
}
