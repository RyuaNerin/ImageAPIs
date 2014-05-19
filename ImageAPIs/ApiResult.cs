using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ImageAPIs
{
	internal class ApiResult : IAsyncResult
	{
		internal ApiObject			apiObject;
		internal ManualResetEvent	manualEvent;

		public WaitHandle	AsyncWaitHandle			{ get { return this.manualEvent; } }
		public bool			IsCompleted				{ get; internal set; }
		public object		AsyncState				{ get; internal set; }
		public bool			CompletedSynchronously	{ get; internal set; }
	}
}
