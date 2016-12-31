using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageAPIs.Search
{

    public class sizebooru : SearchBooru
    {

        public override int EngineID { get { return EngineIDs.eRule34; } }
        public sizebooru()
			: base("http://size.booru.org/index.php?page=dapi&s=post&q=index", "tag", "pid", "limit", false)
		{
        }
    }
}
