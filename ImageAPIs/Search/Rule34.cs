using System;

namespace ImageAPIs.Search
{
	// http://rule34.xxx/index.php?page=help&topic=dapi

	public class Rule34 : SearchBooru
	{
		public override int EngineID { get { return EngineIDs.eRule34; } }

		public Rule34()
			: base("http://rule34.xxx/index.php?page=dapi&s=post&q=index", "tags", "pid", "limit", true)
		{
		}
	}
}
