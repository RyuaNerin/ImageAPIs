using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ImageAPIs.Search
{
	public class EShuushuu : SearchImage
	{
		public override int EngineID { get { return EngineIDs.eEShuushuu; } }

		internal override Uri RequestURL(SearchOption option)
		{
			bool b;
			
			if (option is EshuushuuOptions)
			{
				EshuushuuOptions eoption = option as EshuushuuOptions;
				b = ((eoption.Tags.Length + eoption.Artist.Length + eoption.Characters.Length + eoption.Source.Length) == 0);
			}
			else
			{
				b = (option.Tags.Length == 0);
			}

			if (b)
				if (option.Page != null)
					return new Uri("http://e-shuushuu.net/");
				else
					return new Uri("http://e-shuushuu.net/?page=" + option.Page);
			else
				return new Uri("http://e-shuushuu.net/search/process/");
		}

		internal override byte[] RequestBody(SearchOption option)
		{
			string tags = null;
			string source = null;
			string characters = null;
			string artist = null;

			tags = option.Tags;
			if (option is EshuushuuOptions)
			{
				EshuushuuOptions eoption = option as EshuushuuOptions;
				source = eoption.Source;
				characters = eoption.Characters;
				artist = eoption.Artist;
			}

			if (!String.IsNullOrEmpty(tags) &&
				!String.IsNullOrEmpty(source) &&
				!String.IsNullOrEmpty(characters) &&
				!String.IsNullOrEmpty(artist))
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("postcontent=&txtposter=&");
				sb.AppendFormat("tags={0}&", Uri.EscapeUriString(Helper.GetQuotationMarkedTag(tags)));
				sb.AppendFormat("source={0}&", Uri.EscapeUriString(Helper.GetQuotationMarkedTag(source)));
				sb.AppendFormat("char={0}&", Uri.EscapeUriString(Helper.GetQuotationMarkedTag(characters)));
				sb.AppendFormat("artist={0}", Uri.EscapeUriString(Helper.GetQuotationMarkedTag(artist)));

				return this.Encoding.GetBytes(sb.ToString());
			}
			else
			{
				return null;
			}
		}

		static readonly Regex regSplit	= new Regex("<div class=\"image_thread display\"",			RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly Regex regID		= new Regex("Image #(\\d+)</a>",							RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly Regex regImage	= new Regex("<a class=\"thumb_image\" href=\"([^\"]+)\"",	RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly Regex regThumb	= new Regex("<img src=\"([^\"]+)\"",						RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly Regex regData	= new Regex("<dt>Submitted On:</dt>[^<]+<dd>([^<]+)</dd>",	RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly Regex regSize	= new Regex("<dt>Dimensions:</dt><dd>(\\d+)x(\\d+) \\(",	RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly Regex regTags	= new Regex("Tags:[^<]+</dt>[^<]+<dd[^>]+>(.+)</dd>",		RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly Regex regSource	= new Regex("Source:[^<]+</dt>[^<]+<dd[^>]+>(.+)</dd>",		RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly Regex regArtist	= new Regex("Artist:[^<]+</dt>[^<]+<dd[^>]+>(.+)</dd>",		RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly Regex regChar	= new Regex("Characters:[^<]+</dt>[^<]+<dd[^>]+>(.+)</dd>",	RegexOptions.IgnoreCase | RegexOptions.Compiled);
		static readonly Regex regDelTag	= new Regex("<[^>]+>",										RegexOptions.IgnoreCase | RegexOptions.Compiled);
		
		// May 18th, 2014 11:15 AM

		internal override IList<ImageInfo> ParseData(string body, SearchOption option)
		{
			List<ImageInfo> lstResult = new List<ImageInfo>();

			string[] ss = regSplit.Split(body);

			Match m;

			for (int i = 1; i < ss.Length; ++i)
			{
				ImageInfo info = new ImageInfo();

				info.EngineID		= this.EngineID;
				info.Rating			= Ratings.All;
				info.ImageId		= regID.Match(ss[i]).Groups[1].Value;
				//info.CreatedAt		= DateTime.ParseExact(regData.Match(ss[i]).Groups[1].Value, DataFormat, CultureInfo.InvariantCulture);
				info.CreatedAt		= DateTime.Parse(regData.Match(ss[i]).Groups[1].Value, CultureInfo.InvariantCulture);
				info.Source			= null;

				info.OrigUrl		= regImage.Match(ss[i]).Groups[1].Value;
				info.OrigFileSize	= 0;
				m = regSize.Match(ss[i]);
				info.OrigWidth		= int.Parse(m.Groups[1].Value);
				info.OrigHeight		= int.Parse(m.Groups[2].Value);

				info.SampleUrl		= null;
				info.SampleFileSize	= 0;
				info.SampleWidth	= 0;
				info.SampleHeight	= 0;

				info.ThumbUrl		= regThumb.Match(ss[i]).Groups[1].Value;
				info.ThumbWidth		= 0;
				info.ThumbHeight	= 0;

				info.TagsGeneral	= Helper.MakeTagList(regDelTag.Replace(regTags.Match(ss[i]).Groups[1].Value, ""));
				info.TagsArtist		= Helper.MakeTagList(regDelTag.Replace(regArtist.Match(ss[i]).Groups[1].Value, ""));
				info.TagsCharacter	= Helper.MakeTagList(regDelTag.Replace(regChar.Match(ss[i]).Groups[1].Value, ""));
				info.TagsSource		= Helper.MakeTagList(regDelTag.Replace(regSource.Match(ss[i]).Groups[1].Value, ""));

				List<string> lst = new List<string>();
				lst.AddRange(info.TagsGeneral);
				lst.AddRange(info.TagsArtist);
				lst.AddRange(info.TagsCharacter);
				lst.AddRange(info.TagsSource);

				info.TagsAll		= lst.AsReadOnly();
				info.TagsCopyRight	= null;

				lstResult.Add(info);
			}

			return lstResult.AsReadOnly();
		}
	}
}
