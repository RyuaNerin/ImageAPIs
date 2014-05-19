using System;

namespace ImageAPIs.Search
{
	// http://chan.sankakucomplex.com/wiki/show?title=help%3A_api_v1.13.0

	public class ChanSankaku : SearchBooru
	{
		public override int EngineID { get { return EngineIDs.eChanSankaku; } }

		public ChanSankaku()
			: base("http://chan.sankakucomplex.com/post/index.json", "tags", "page", "limit", true)
		{
		}
	}
}
