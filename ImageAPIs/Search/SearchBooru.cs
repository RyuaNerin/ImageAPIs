using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ComputerBeacon.Json;

namespace ImageAPIs.Search
{
	public abstract class SearchBooru : SearchImage
	{
		private readonly string	BaseURL;
		private readonly string	SearchURL;
		private readonly bool	isJson;

		private readonly string	TagsName;
		private readonly string	PageName;
		private readonly string	LimitName;

		public SearchBooru(string searchURL, string tagName, string pageName, string limitName, bool isJson)
		{
			this.BaseURL = new Uri(searchURL).GetLeftPart(UriPartial.Authority);
			if (this.BaseURL[this.BaseURL.Length - 1] == '/')
				this.BaseURL = this.BaseURL.Substring(0, this.BaseURL.Length - 1);

			this.SearchURL = searchURL;
			this.TagsName = searchURL;
			this.PageName = pageName;
			this.LimitName = limitName;
			this.isJson = isJson;
		}

		internal override byte[] RequestBody(SearchOption option)
		{
			return null;
		}

		internal override Uri RequestURL(SearchOption option)
		{
			StringBuilder sbQuery = new StringBuilder();

			sbQuery.Append(this.SearchURL);
			if (this.SearchURL.IndexOf('?') < 0)
				sbQuery.Append('?');
			else
				sbQuery.Append('&');

			if (!String.IsNullOrEmpty(option.Tags))
				sbQuery.AppendFormat("{0}={1}&", TagsName, Uri.EscapeUriString(option.Tags));

			if (option.Page != null)
				sbQuery.AppendFormat("{0}={1}&", PageName, option.Page.Value);

			if (option.Limit != null)
				sbQuery.AppendFormat("{0}={1}&", LimitName, option.Limit.Value);

			if (sbQuery[sbQuery.Length - 1] == '&')
				sbQuery.Remove(sbQuery.Length - 1, 1);

			if (sbQuery[sbQuery.Length - 1] == '?')
				sbQuery.Remove(sbQuery.Length - 1, 1);

			return new Uri(sbQuery.ToString());
		}

		internal override IList<ImageInfo> ParseData(string body, SearchOption option)
		{
			if (this.isJson)
				return this.ParseDataJson(body, option);
			else
				return this.ParseDataXml(body, option);
		}

		private IList<ImageInfo> ParseDataJson(string body, SearchOption option)
		{
			List<ImageInfo> lstResult = new List<ImageInfo>();

			JsonArray jsonArray = new JsonArray(body);

			for (int i = 0; i < jsonArray.Count; ++i)
			{
				JsonObject jsonObject = jsonArray[i] as JsonObject;
				if (jsonObject != null)
				{
					ImageInfo info = new ImageInfo();

					info.EngineID = this.EngineID;
					info.Rating = Helper.ToRating(jsonObject.GetString("q"));
					if (Helper.CompareRating(option.Rating, info.Rating))
					{
						info.ImageId		= jsonObject.GetString("id");
						info.CreatedAt		= jsonObject.GetDateTime("created_at");
						info.Source			= jsonObject.GetString("source");

						info.TagsAll		= jsonObject.GetTagList("tags", "tag_string");
						info.TagsArtist		= jsonObject.GetTagList("tag_string_artist");
						info.TagsCharacter	= jsonObject.GetTagList("tag_string_character");
						info.TagsCopyRight	= jsonObject.GetTagList("tag_string_copyright");
						info.TagsGeneral	= jsonObject.GetTagList("tag_string_general");
						info.TagsSource		= null;

						info.OrigUrl		= jsonObject.GetString("file_url");
						info.OrigFileSize	= jsonObject.GetInt64("file_size");
						info.OrigWidth		= jsonObject.GetInt32("width");
						info.OrigHeight		= jsonObject.GetInt32("height");

						info.SampleUrl		= jsonObject.GetString("sample_url");
						info.SampleFileSize	= jsonObject.GetInt64("sample_file_size");
						info.SampleWidth	= jsonObject.GetInt32("sample_height");
						info.SampleHeight	= jsonObject.GetInt32("sample_width");

						info.ThumbUrl		= jsonObject.GetString("preview_url");
						info.ThumbWidth		= jsonObject.GetInt32("preview_width");
						info.ThumbHeight	= jsonObject.GetInt32("preview_height");

						this.CheckURL(info);

						lstResult.Add(info);
					}
				}
			}

			return lstResult.AsReadOnly();
		}
		
		private IList<ImageInfo> ParseDataXml(string body, SearchOption option)
		{
			List<ImageInfo> lstResult = new List<ImageInfo>();

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(body);

			XmlNodeList postNodes = xmlDocument.GetElementsByTagName("post");

			foreach (XmlNode postNode in postNodes)
			{
				ImageInfo info = new ImageInfo();

				info.EngineID = this.EngineID;
				info.Rating = Helper.ToRating(postNode.GetString("q"));
				if (Helper.CompareRating(option.Rating, info.Rating))
				{
					info.ImageId		= postNode.GetString("id");
					info.CreatedAt		= postNode.GetDateTime("created_at");
					info.Source			= postNode.GetString("source");

					info.TagsAll		= postNode.GetTagList("tags", "tag_string");
					info.TagsArtist		= postNode.GetTagList("tag_string_artist");
					info.TagsCharacter	= postNode.GetTagList("tag_string_character");
					info.TagsCopyRight	= postNode.GetTagList("tag_string_copyright");
					info.TagsGeneral	= postNode.GetTagList("tag_string_general");
					info.TagsSource		= null;

					info.OrigUrl		= postNode.GetString("file_url");
					info.OrigFileSize	= postNode.GetInt64("file_size");
					info.OrigWidth		= postNode.GetInt32("width");
					info.OrigHeight		= postNode.GetInt32("height");

					info.SampleUrl		= postNode.GetString("sample_url");
					info.SampleFileSize	= postNode.GetInt64("sample_file_size");
					info.SampleWidth	= postNode.GetInt32("sample_width");
					info.SampleHeight	= postNode.GetInt32("sample_height");

					info.ThumbUrl		= postNode.GetString("preview_url");
					info.ThumbWidth		= postNode.GetInt32("preview_width");
					info.ThumbHeight	= postNode.GetInt32("preview_height");

					this.CheckURL(info);

					lstResult.Add(info);
				}
			}

			return lstResult.AsReadOnly();
		}

		private void CheckURL(ImageInfo info)
		{
			if (!info.OrigUrl.StartsWith("http://") && !info.OrigUrl.StartsWith("https://"))
				info.OrigUrl = this.BaseURL + info.OrigUrl;

			if (!info.SampleUrl.StartsWith("http://") && !info.SampleUrl.StartsWith("https://"))
				info.SampleUrl = this.BaseURL + info.SampleUrl;

			if (!info.ThumbUrl.StartsWith("http://") && !info.ThumbUrl.StartsWith("https://"))
				info.ThumbUrl = this.BaseURL + info.ThumbUrl;
		}
	}
}
