namespace SportsStore.Infrastructure;

public static class UrlExtentions
{
    public static string PathAndQuery(this HttpRequest request)
    {
        return request.QueryString.HasValue
        ? $"{request.Path}{request.QueryString}"
        : request.Path.ToString();
    }

}
