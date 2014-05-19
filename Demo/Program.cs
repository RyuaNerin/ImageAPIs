using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ImageAPIs;

namespace ImageAPIsDemo
{
	class Program
	{
		static SearchImage si = new ImageAPIs.Search.Yandere();

		static void Main(string[] args)
		{
			IAsyncResult result = si.BeginSearch(new SearchOption("tsutsukakushi_tsukiko"), Callback, 0);

			while (true)
			{
				Thread.Sleep(100);
			}
		}

		static void Callback(IAsyncResult async)
		{
			SearchResult res = si.EndSearch(async);

			foreach (ImageInfo info in res.Result)
			{
				Console.Clear();
				Console.WriteLine("CreatedAt: {0}", info.CreatedAt);
				Console.WriteLine("ImageId: {0}", info.ImageId);

				Console.WriteLine("OrigUrl: {0}", info.OrigUrl);
				Console.WriteLine("OrigFileSize: {0}", info.OrigFileSize);
				Console.WriteLine("OrigWidth: {0}", info.OrigWidth);
				Console.WriteLine("OrigHeight: {0}", info.OrigHeight);

				Console.WriteLine("SampleUrl: {0}", info.SampleUrl);
				Console.WriteLine("SampleFileSize: {0}", info.SampleFileSize);
				Console.WriteLine("SampleWidth: {0}", info.SampleWidth);
				Console.WriteLine("SampleHeight: {0}", info.SampleHeight);

				Console.WriteLine("ThumbUrl: {0}", info.ThumbUrl);
				Console.WriteLine("ThumbWidth: {0}", info.ThumbWidth);

				Console.WriteLine("ThumbHeight: {0}", info.ThumbHeight);

				Console.WriteLine("TagsAll.Count: {0}", info.TagsAll.Count);

				Console.ReadKey();
			}
		}
	}
}
