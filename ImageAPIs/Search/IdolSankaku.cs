using System;

namespace ImageAPIs.Search
{
	// http://idol.sankakucomplex.com/wiki/show?title=help%3A_api_v1.13.0

	public class IdolSankaku : SearchBooru
	{
		public override int EngineID { get { return EngineIDs.eIdolSankaku; } }

		public IdolSankaku()
			: base("http://idol.sankakucomplex.com/post/index.json", "tags", "page", "limit", true)
		{
		}
	}
}
