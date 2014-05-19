using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ComputerBeacon.Json;
using System.Xml;
using System.IO;

namespace ImageAPIs
{
	internal static class Helper
	{
		private static readonly DateTime DateTime1970 = new DateTime(1970, 1, 1, 0, 0, 0);
		public static DateTime ParseDateTime(long date)
		{
			return DateTime.SpecifyKind(DateTime1970.AddSeconds(date), DateTimeKind.Local);
		}
		private static readonly string DateFormat =  "ddd MMM dd HH:mm:ss zzz yyyy";
		public static DateTime ParseDateTime(string data)
		{
			return DateTime.ParseExact(data, DateFormat, CultureInfo.InvariantCulture);
		}
		public static string GetQuotationMarkedTag(string tag)
		{
			if (tag.Length > 0)
			{
				StringBuilder sb = new StringBuilder(tag.Length);
				sb.Append("\"");
				for (int i = 0; i < tag.Length; i++)
					if (tag[i] == ' ')
						sb.Append("\" \"");
					else
						sb.Append(tag[i]);
				sb.Append("\"");

				return sb.ToString();
			}
			else
			{
				return null;
			}
		}
		public static string StringFromStream(Stream stream)
		{
			string r;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] buff = new byte[4096];
				int read;

				while ((read = stream.Read(buff, 0, 4096)) > 0)
					memoryStream.Write(buff, 0, read);
				memoryStream.Flush();

				r = Encoding.UTF8.GetString(memoryStream.ToArray());

				memoryStream.Dispose();
			}

			return r;
		}

		public static IList<string> MakeTagList()
		{
			return new List<string>().AsReadOnly();
		}
		public static IList<string> MakeTagList(string tags)
		{
			if (String.IsNullOrEmpty(tags))
				return null;
			else
				return new List<string>(tags.Trim().Split(' ')).AsReadOnly();
		}

		public static Ratings ToRating(string rating)
		{
			switch (rating)
			{
				case "s": return Ratings.Safe;
				case "q": return Ratings.Questionable;
				case "e": return Ratings.Explicit;
			}

			return Ratings.All;
		}

		public static bool CompareRating(Ratings searchRating, string targetRating)
		{
			return CompareRating(searchRating, ToRating(targetRating));
		}
		public static bool CompareRating(Ratings searchRating, Ratings targetRating)
		{
			return targetRating <= searchRating;
		}

		public static string GetString(this JsonObject jsonObject, params string[] key)
		{
			for (int i = 0; i < key.Length; ++i)
				if (jsonObject.ContainsKey(key[i]))
					return Convert.ToString(jsonObject[key[i]]);

			return null;
		}
		public static int GetInt32(this JsonObject jsonObject, params string[] key)
		{
			for (int i = 0; i < key.Length; ++i)
				if (jsonObject.ContainsKey(key[i]))
					return Convert.ToInt32(jsonObject[key[i]]);

			return 0;
		}
		public static long GetInt64(this JsonObject jsonObject, params string[] key)
		{
			for (int i = 0; i < key.Length; ++i)
				if (jsonObject.ContainsKey(key[i]))
					return Convert.ToInt64(jsonObject[key[i]]);

			return 0;
		}
		public static DateTime GetDateTime(this JsonObject jsonObject, params string[] key)
		{
			object obj;
			for (int i = 0; i < key.Length; ++i)
			{
				if (jsonObject.ContainsKey(key[i]))
				{
					obj = jsonObject[key[i]];

					if (obj is int || obj is long)
						return ParseDateTime(Convert.ToInt64(obj));
					else
						return ParseDateTime(Convert.ToString(obj));
				}
			}

			return DateTime.MinValue;
		}
		public static IList<string> GetTagList(this JsonObject jsonObject, params string[] key)
		{
			return MakeTagList(jsonObject.GetString(key));
		}

		public static string GetString(this XmlNode node, params string[] key)
		{
			XmlAttribute attr;
			for (int i = 0; i < key.Length; ++i)
			{
				attr = node.Attributes[key[i]];
				if (attr != null)
					return attr.Value;
			}

			return null;
		}
		public static int GetInt32(this XmlNode node, params string[] key)
		{
			XmlAttribute attr;
			for (int i = 0; i < key.Length; ++i)
			{
				attr = node.Attributes[key[i]];
				if (attr != null)
					return Convert.ToInt32(attr.Value);
			}

			return 0;
		}
		public static long GetInt64(this XmlNode node, params string[] key)
		{
			XmlAttribute attr;
			for (int i = 0; i < key.Length; ++i)
			{
				attr = node.Attributes[key[i]];
				if (attr != null)
					return Convert.ToInt64(attr.Value);
			}

			return 0;
		}
		public static DateTime GetDateTime(this XmlNode node, params string[] key)
		{
			XmlAttribute attr;
			int n;
			long l;
			for (int i = 0; i < key.Length; ++i)
			{
				attr = node.Attributes[key[i]];

				if (attr != null)
					if (int.TryParse(attr.Value, out n))
						return ParseDateTime((long)n);
					else if (long.TryParse(attr.Value, out l))
						return ParseDateTime(l);
					else
						return ParseDateTime(attr.Value);
			}

			return DateTime.MinValue;
		}
		public static IList<string> GetTagList(this XmlNode node, params string[] key)
		{
			return MakeTagList(node.GetString(key));
		}
	}
}
