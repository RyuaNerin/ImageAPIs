# ImageApis
- Image searching library in GelBooru, DanBooru, E-Shuushuu, and many others.
- For .NET Framework 3.5
- CopyRight (C) 2014, RyuaNerin

# OpenSource Library
- [JsonTookit 4.1.736 (Ms-PL)](http://jsontoolkit.codeplex.com/)

# Example
##### Find Image
```
SearchImage sImage = new ImageAPIs.Search.Gelbooru();
SerachOption sOption = new SearchOption("tsutsukakushi_tsukiko");
IList<ImageInfo> lstImages = sImage.Search(sOption);
```
##### Find Image by Async
```
void FindImage()
{
    SearchImage sImage = new ImageAPIs.Search.Gelbooru();
    SerachOption sOption = new SearchOption("tsutsukakushi_tsukiko");
    sImage.SearchAsync(sOption, Callback, sImage);
}

void Callback(IAsyncResult asyncResult)
{
	SearchImage sImage = asyncResult.UserState as SearchImage;
	SearchResult result = sImage.EndAsync(asyncResult);
}
```
##### Add new BooruApi
```
public class Danbooru : SearchBooru
{
	public override int EngineID { get { return EngineIDs.eDanbooru; } }

	public Danbooru()
		: base("http://danbooru.donmai.us/posts.json", "tags", "page", "limit", true)
	{
	}
}
```

