using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace ImageAPIs
{
	public abstract class SearchImage
	{
		public abstract int EngineID { get; }

		private IWebProxy m_webProxy;
		private Encoding m_Encoding;

		public SearchImage()
		{
			this.m_Encoding = Encoding.UTF8;
		}

		public Encoding Encoding
		{
			get { return this.m_Encoding; }
			set { this.m_Encoding = value; }
		}

		public IWebProxy Proxy
		{
			get { return this.m_webProxy; }
			set { this.m_webProxy = value; }
		}

		public IList<ImageInfo> Search(SearchOption option)
		{
			Uri uriRequest = this.RequestURL(option);
			byte[] uriBody = this.RequestBody(option);

			HttpWebRequest wReq = WebRequest.Create(this.RequestURL(option)) as HttpWebRequest;
			wReq.Method = (uriBody != null) ? "POST" : "GET";
			if (uriBody != null)
			{
				wReq.ContentType = "application/x-www-form-urlencoded";
				wReq.GetRequestStream().Write(uriBody, 0, uriBody.Length);
			}

			HttpWebResponse	wRes = wReq.GetResponse() as HttpWebResponse;
			
			string body = Helper.StringFromStream(wRes.GetResponseStream());

			wRes.Close();

			return this.ParseData(body, option);
		}
		public IAsyncResult BeginSearch(SearchOption option, AsyncCallback callBack)
		{
			return this.BeginSearch(option, callBack, null);
		}
		public IAsyncResult BeginSearch(SearchOption option, AsyncCallback callBack, object UserState)
		{
			Uri uriRequest = this.RequestURL(option);
			byte[] uriBody = this.RequestBody(option);

			ApiResult asyncApi = new ApiResult();

			asyncApi.apiObject = new ApiObject();
			asyncApi.apiObject.callback = callBack;
			asyncApi.apiObject.option = option;
			asyncApi.apiObject.userState = UserState;
			
			asyncApi.apiObject.hwReq = WebRequest.Create(this.RequestURL(option)) as HttpWebRequest;
			asyncApi.apiObject.hwReq.Method = (uriBody != null) ? "POST" : "GET";
			if (uriBody != null)
			{
				asyncApi.apiObject.hwReq.ContentType = "application/x-www-form-urlencoded";
				asyncApi.apiObject.hwReq.GetRequestStream().Write(uriBody, 0, uriBody.Length);
			}

			asyncApi.apiObject.asyncHttp = asyncApi.apiObject.hwReq.BeginGetResponse(RequestCallback, asyncApi.apiObject);

			asyncApi.manualEvent = new ManualResetEvent(false);

			asyncApi.AsyncState = UserState;
			asyncApi.IsCompleted = false;
			asyncApi.CompletedSynchronously = false;

			
			asyncApi.apiObject.asyncApi = asyncApi;

			return asyncApi;
		}
		public SearchResult EndSearch(IAsyncResult asyncResult)
		{
			ApiResult aRes = asyncResult as ApiResult;
			if (aRes == null)
				throw new FormatException();

			SearchResult args = new SearchResult();

			try
			{
				args.Result = this.ParseData(aRes.apiObject.Body, aRes.apiObject.option);
				args.Success = true;
				args.Error = null;
			}
			catch
			{
				args.Result = null;
				args.Success = false;
				args.Error = null;
			}

			return args;
		}

		public void CancleAsync(IAsyncResult asyncResult)
		{
			ApiResult apiResult = asyncResult as ApiResult;
			if (apiResult == null)
				throw new FormatException();

			apiResult.CompletedSynchronously = true;

			apiResult.apiObject.hwReq.Abort();
		}

		private void RequestCallback(IAsyncResult asyncResult)
		{
			ApiObject apiObject = (ApiObject)asyncResult.AsyncState;

			HttpWebResponse hwRes = apiObject.hwReq.EndGetResponse(apiObject.asyncHttp) as HttpWebResponse;

			apiObject.Body = Helper.StringFromStream(hwRes.GetResponseStream());
			hwRes.Close();

			apiObject.asyncApi.manualEvent.Set();
			apiObject.asyncApi.IsCompleted = true;

			apiObject.callback.Invoke(apiObject.asyncApi);
		}

		internal abstract Uri RequestURL(SearchOption option);
		internal abstract byte[] RequestBody(SearchOption option);
		internal abstract IList<ImageInfo> ParseData(string body, SearchOption option);

		public AsyncResult AsyncResult
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}
	}
}
