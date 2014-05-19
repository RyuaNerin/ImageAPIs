using System;

namespace ImageAPIs.Search
{
	// http://www.gelbooru.com/index.php?page=dapi&s=post&q=index

	public class Gelbooru : SearchBooru
	{
		public override int EngineID { get { return EngineIDs.eGelbooru; } }

		public Gelbooru()
			: base("http://www.gelbooru.com/index.php?page=dapi&s=post&q=index", "tags", "pid", "limit", false)
		{
		}
	}
}
