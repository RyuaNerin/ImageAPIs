using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageAPIs
{
	public class ImageInfo
	{
		public int		EngineID		{ get; internal set; }

		public string	ImageId			{ get; internal set; }
		public Ratings	Rating			{ get; internal set; }
		public DateTime CreatedAt		{ get; internal set; }

		public string	Source			{ get; internal set; }

		public string	OrigUrl			{ get; internal set; }
		public int		OrigWidth		{ get; internal set; }
		public int		OrigHeight		{ get; internal set; }
		public long		OrigFileSize	{ get; internal set; }

		public string	SampleUrl		{ get; internal set; }
		public long		SampleFileSize	{ get; internal set; }
		public int		SampleWidth		{ get; internal set; }
		public int		SampleHeight	{ get; internal set; }

		public string	ThumbUrl		{ get; internal set; }
		public int		ThumbWidth		{ get; internal set; }
		public int		ThumbHeight		{ get; internal set; }

		public IList<string> TagsAll		{ get; internal set; }
		public IList<string> TagsGeneral	{ get; internal set; }
		public IList<string> TagsArtist		{ get; internal set; }
		public IList<string> TagsCharacter	{ get; internal set; }
		public IList<string> TagsCopyRight	{ get; internal set; }
		public IList<string> TagsSource		{ get; internal set; }
	}
}
