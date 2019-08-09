namespace Sitecore.Support.XA.Feature.Redirects.Pipelines.HttpRequest
{
    using Sitecore;
    using Sitecore.XA.Feature.Redirects.Pipelines.HttpRequest;
    using System;
    using System.Web;

    public class RedirectMapResolver : Sitecore.XA.Feature.Redirects.Pipelines.HttpRequest.RedirectMapResolver
    {
        protected override string GetTargetUrl(RedirectMapping mapping, string input)
        {
            string text = mapping.Target;
            if (mapping.IsRegex)
            {
                text = mapping.Regex.Replace(input, text);
            }
            if (mapping.PreserveQueryString)
            {
                text += HttpContext.Current.Request.Url.Query;
            }
            if (!Uri.IsWellFormedUriString(text, UriKind.Absolute))
            {
                if (!string.IsNullOrEmpty(Context.Site.VirtualFolder))
                {
                    text = StringUtil.EnsurePostfix('/', Context.Site.VirtualFolder) + text.TrimStart('/');
                }
            }
            return text;
        }
    }
}