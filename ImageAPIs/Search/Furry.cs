using System;

namespace ImageAPIs.Search
{
	// http://furry.booru.org/index.php?page=help&topic=dapi

	public class Furry : SearchBooru
	{
		public override int EngineID { get { return EngineIDs.eFurry; } }

		public Furry()
			: base("http://furry.booru.org/index.php?page=dapi&s=post&q=index", "tag", "pid", "limit", false)
		{
		}
	}
}
