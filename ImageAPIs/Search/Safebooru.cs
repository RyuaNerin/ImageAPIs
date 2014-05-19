using System;

namespace ImageAPIs.Search
{
	// http://safebooru.org/index.php?page=help&topic=dapi

	public class Safebooru : SearchBooru
	{
		public override int EngineID { get { return EngineIDs.eSafebooru; } }

		public Safebooru()
			: base("http://safebooru.org/index.php?page=dapi&s=post&q=index", "tags", "pid", "limit", false)
		{
		}
	}
}
