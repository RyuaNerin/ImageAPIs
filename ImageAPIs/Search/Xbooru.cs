using System;

namespace ImageAPIs.Search
{
	// http://xbooru.com/index.php?page=help&topic=dapi

	public class Xbooru : SearchBooru
	{
		public override int EngineID { get { return EngineIDs.eXbooru; } }

		public Xbooru()
			: base("http://xbooru.com/index.php?page=dapi&s=post&q=index", "tags", "pid", "limit", false)
		{
		}
	}
}
