using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ImageAPIs
{
	internal class ApiObject
	{
		public HttpWebRequest	hwReq;
		public ApiResult		asyncApi;
		public IAsyncResult		asyncHttp;
		public AsyncCallback	callback;
		public SearchOption		option;
		public object			userState;
		public string			Body;
	}
}
