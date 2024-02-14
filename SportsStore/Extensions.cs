namespace SportsStore;

public static class Extensions
{
	public static string GetUrl(this HttpContext context) => context.Request.QueryString.HasValue ?
		context.Request.Path.ToString() + context.Request.QueryString.ToString() :
		context.Request.Path.ToString();
}
